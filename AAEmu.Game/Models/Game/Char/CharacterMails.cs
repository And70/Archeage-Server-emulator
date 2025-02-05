﻿using System;
using System.Collections.Generic;

using AAEmu.Game.Core.Managers;
using AAEmu.Game.Core.Packets.G2C;
using AAEmu.Game.Models.Game.Items;
using AAEmu.Game.Models.Game.Items.Actions;
using AAEmu.Game.Models.Game.Mails;

namespace AAEmu.Game.Models.Game.Char
{
    public class CharacterMails
    {
        public Character Self { get; set; }
        public CountUnreadMail UnreadMailCount { get; set; }

        public CharacterMails(Character self)
        {
            Self = self;

            UnreadMailCount = new CountUnreadMail
            {
                TotalSent = 0,
                TotalReceived = 0,
                TotalMiaReceived = 0,
                TotalCommercialReceived = 0
            };
        }

        public void OpenMailbox()
        {
            var total = 0;
            foreach (var m in MailManager.Instance.GetCurrentMailList(Self))
            {
                if (m.Value.Header.SenderId == Self.Id && m.Value.Header.ReceiverId == Self.Id)
                {
                    Self.SendPacket(new SCMailListPacket(false, new MailHeader[] { m.Value.Header }));
                    total++;
                }
                else if (m.Value.Header.SenderId == Self.Id)
                {
                    Self.SendPacket(new SCMailListPacket(true, new MailHeader[] { m.Value.Header }));
                    total++;
                }
                else if (m.Value.Header.ReceiverId == Self.Id)
                {
                    Self.SendPacket(new SCMailListPacket(false, new MailHeader[] { m.Value.Header }));
                    total++;
                }
            }
            Self.SendPacket(new SCMailListEndPacket((byte)total, UnreadMailCount));
        }

        public void ReadMail(bool isSent, long id)
        {
            if (MailManager.Instance._allPlayerMails.ContainsKey(id))
            {
                if (MailManager.Instance._allPlayerMails[id].Header.Status == 0 && !isSent)
                {
                    UnreadMailCount.TotalReceived -= 1;
                    MailManager.Instance._allPlayerMails[id].Header.OpenDate = DateTime.UtcNow;
                    MailManager.Instance._allPlayerMails[id].Header.Status = 1;
                }
                Self.SendPacket(new SCMailBodyPacket(false, isSent, MailManager.Instance._allPlayerMails[id].Body, true, UnreadMailCount));
                Self.SendPacket(new SCMailStatusUpdatedPacket(isSent, id, MailManager.Instance._allPlayerMails[id].Header.Status));
                Self.SendPacket(new SCCountUnreadMailPacket(UnreadMailCount));
            }
        }

        public bool SendMail(byte type, string receiverName, string senderName, string title, string text, byte attachments, int money0, int money1, int money2, long extra, List<(Items.SlotType, byte)> itemSlots)
        {
            var mailTemplate = new Mail
            {
                Id = MailManager.Instance.highestMailID += 1
            };
            uint senderId;
            uint receiverId;

            mailTemplate.Header = new MailHeader()
            {
                mailId = mailTemplate.Id,
                Type = type,
                Status = 0,
                Title = title,
                SenderName = senderName,
                Attachments = attachments,
                ReceiverName = receiverName,
                OpenDate = DateTime.MinValue,
                Returned = 0,
                Extra = extra
            };
            if (senderName != "")
            {
                senderId = NameManager.Instance.GetCharacterId(senderName);
                mailTemplate.Header.SenderId = senderId;
            }
            if (receiverName != "")
            {
                receiverId = NameManager.Instance.GetCharacterId(receiverName);
                mailTemplate.Header.ReceiverId = receiverId;
            }

            // Calculate mail fee
            var mailFee = 0;
            var attachmentCost = 30;
            var attachmentCountForFee = 0;
            foreach (var mailSlots in itemSlots)
            {
                if (mailSlots.Item1 != 0)
                {
                    attachmentCountForFee++;
                }
            }

            if (money0 > 0)
            {
                attachmentCountForFee++;
            }

            if (mailTemplate.Header.Type == 1) // Normal
            {
                mailFee += 50;
            }
            else if (mailTemplate.Header.Type == 2) // Express
            {
                mailFee += 100;
                attachmentCost = 80;
            }
            if (attachmentCountForFee > 1)
            {
                mailFee += (attachmentCountForFee - 1) * attachmentCost;
            }

            if (mailFee > Self.Money)
            {
                Self.SendErrorMessage(ErrorMessageType.MailNotEnoughMoney);
                return false;
            }

            var mailItemIds = new List<ulong>();
            foreach (var mailSlots in itemSlots)
            {
                if (mailSlots.Item1 != 0)
                {
                    var tempItem = Self.Inventory.GetItem(mailSlots.Item1, mailSlots.Item2);
                    if (tempItem.SlotType == SlotType.Inventory)
                    {
                        Self.Inventory.MailAttachments.AddOrMoveExistingItem(ItemTaskType.Invalid, tempItem);
                        Self.SendPacket(new SCItemTaskSuccessPacket(ItemTaskType.Mail, new List<ItemTask>() { new ItemRemove(tempItem) }, new List<ulong>()));
                        mailItemIds.Add(tempItem.Id);
                    }
                }
            }
            mailTemplate.Body = new MailBody()
            {
                mailId = mailTemplate.Id,
                Type = mailTemplate.Header.Type,
                ReceiverName = mailTemplate.Header.ReceiverName,
                Title = mailTemplate.Header.Title,
                Text = text,
                MoneyAmount1 = money0,
                MoneyAmount2 = money1,
                MoneyAmount3 = money2,
                SendDate = DateTime.UtcNow,
                RecvDate = DateTime.UtcNow,
                OpenDate = mailTemplate.Header.OpenDate,
            };
            foreach (var iId in mailItemIds)
            {
                var i = ItemManager.Instance.GetItemByItemId(iId);
                if (i != null)
                {
                    // i.OwnerId = receiverId;
                    i.SlotType = SlotType.Mail;
                    i.Slot = mailTemplate.Body.Attachments.Count;
                    mailTemplate.Body.Attachments.Add(i);
                }
            }


            Self.ChangeMoney(SlotType.Inventory, -mailFee);
            MailManager.Instance._allPlayerMails.Add(mailTemplate.Id, mailTemplate);
            Self.SendPacket(new SCMailSentPacket(mailTemplate.Header, UnreadMailCount, itemSlots.ToArray()));

            MailManager.Instance.NotifyNewMailByNameIfOnline(mailTemplate, receiverName);
            return true;
        }

        public void GetAttached(long mailId, bool takeMoney, bool takeItems, bool takeAllSelected)
        {
            if (MailManager.Instance._allPlayerMails.TryGetValue(mailId, out var thisMail))
            {
                if (thisMail.Body.MoneyAmount1 > 0 && takeMoney)
                {
                    Self.ChangeMoney(SlotType.Inventory, thisMail.Body.MoneyAmount1);
                    thisMail.Body.MoneyAmount1 = 0;
                    thisMail.Header.Attachments -= 1;
                }
                //var itemIDList = new List<ulong>();
                //var itemSlotList = new List<(SlotType, byte)>();
                var itemSlotList = new List<ItemIdAndLocation>();
                if (takeItems)
                {
                    var toRemove = new List<Item>();
                    foreach (var itemAttachment in thisMail.Body.Attachments)
                    {
                        if (itemAttachment.Id != 0)
                        {
                            if (Self.Inventory.Bag.FreeSlotCount > 0)
                            {
                                Self.Inventory.Bag.AddOrMoveExistingItem(ItemTaskType.Mail, itemAttachment);
                                var iial = new ItemIdAndLocation
                                {
                                    Id = itemAttachment.Id,
                                    SlotType = itemAttachment.SlotType,
                                    Slot = (byte)itemAttachment.Slot
                                };
                                itemSlotList.Add(iial);
                                //itemSlotList.Add((itemAttachment.SlotType, (byte)itemAttachment.Slot));
                                //itemIDList.Add(itemAttachment.Id);
                                thisMail.Header.Attachments -= 1;
                                toRemove.Add(itemAttachment);
                            }
                        }
                    }
                    // thisMail.Body.Attachments.Clear();
                    foreach (var ia in toRemove)
                    {
                        thisMail.Body.Attachments.Remove(ia);
                    }
                }
                Self.SendPacket(new SCAttachmentTakenPacket(mailId, takeMoney, false, takeAllSelected, itemSlotList));
                // Self.SendPacket(new SCAttachmentTakenPacket(mailId, takeMoney, false, takeAllSelected, itemIDList.ToArray(), itemSlotList.ToArray()));
                Self.SendPacket(new SCMailStatusUpdatedPacket(false, mailId, 1));
                // TODO: if source player is online, update their mail info
            }
        }

        public void DeleteMail(long id, bool isSent)
        {
            if (MailManager.Instance._allPlayerMails.ContainsKey(id) && !isSent)
            {
                if (MailManager.Instance._allPlayerMails[id].Header.Attachments <= 0)
                {
                    if (MailManager.Instance._allPlayerMails[id].Header.Status == 0)
                    {
                        Self.SendPacket(new SCMailDeletedPacket(isSent, id, true, UnreadMailCount));
                    }
                    else
                    {
                        Self.SendPacket(new SCMailDeletedPacket(isSent, id, false, UnreadMailCount));
                    }

                    MailManager.Instance.DeleteMail(id);
                }
            }
        }

        public void ReturnMail(long id)
        {
            if (MailManager.Instance._allPlayerMails.ContainsKey(id))
            {
                var thisMail = MailManager.Instance._allPlayerMails[id];
                var itemSlots = new List<(SlotType slotType, byte slot)>();
                for (var i = 0; i < MailBody.MaxMailAttachments; i++)
                {
                    var item = ItemManager.Instance.GetItemByItemId(thisMail.Body.Attachments[i].Id);
                    if (item.SlotType == SlotType.None)
                    {
                        itemSlots.Add((0, 0));
                    }
                    else
                    {
                        itemSlots.Add((item.SlotType, (byte)item.Slot));
                    }
                }

                SendMail(thisMail.Header.Type, thisMail.Header.SenderName, thisMail.Header.ReceiverName, thisMail.Header.Title, thisMail.Body.Text,
                    thisMail.Header.Attachments, thisMail.Body.MoneyAmount1, thisMail.Body.MoneyAmount2, thisMail.Body.MoneyAmount3,
                        thisMail.Header.Extra, itemSlots);

                DeleteMail(id, false);
            }
        }
    }
}
