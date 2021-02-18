﻿namespace AAEmu.Game.Core.Packets.C2G
{
    public static class CSOffsets
    {
        // All opcodes here are updated for version AR3030
        public const ushort X2EnterWorldPacket = 0x000;
        // double _01_&_05_
        public const ushort CSHgResponsePacket = 0x15d;       // level = 1
        public const ushort CSAesXorKeyPacket = 0x163;        // level = 1
        public const ushort CSResturnAddrsPacket = 0x186;     // level = 1
        public const ushort CSHgResponse_05_Packet = 0x15d;   // level = 5
        public const ushort CSAesXorKey_05_Packet = 0x163;    // level = 5
        public const ushort CSResturnAddrs_05_Packet = 0x186; // level = 5

        public const ushort CSTodayAssignmentPacket = 0x17a;
        public const ushort CSRequestSkipClientDrivenIndunPacket = 0x04c;
        public const ushort CSRemoveClientNpcPacket = 0x0b0;
        public const ushort CSMoveUnitPacket = 0x084;
        public const ushort CSCofferInteractionPacket = 0x11a;
        public const ushort CSRequestCommonFarmListPacket = 0x18f;
        public const ushort CSChallengeDuelPacket = 0x0ee;
        public const ushort CSStartDuelPacket = 0x008;
        public const ushort CSHeroRankingListPacket = 0x06a;
        public const ushort CSHeroCandidateListPacket = 0x02f;
        public const ushort CSHeroAbstainPacket = 0x189;
        public const ushort CSHeroVotingPacket = 0x0d6;
        public const ushort CSConvertItemLookPacket = 0x0a7;
        public const ushort CSConvertItemLook2Packet = 0x09;
        public const ushort CSSetPingPosPacket = 0x02e;
        public const ushort CSUpdateExploredRegionPacket = 0x1a0;
        public const ushort CSIcsMoneyRequestPacket = 0x183;
        public const ushort CSPremiumServiceBuyPacket = 0x174;
        public const ushort CSSetVisiblePremiumServicePacket = 0x007;
        public const ushort CSAddReputationPacket = 0x0f4;
        public const ushort CSUnknown0x80Packet = 0x080;
        public const ushort CSGetResidentDescPacket = 0x190;
        public const ushort CSRefreshResidentMembersPacket = 0x133;
        public const ushort CSGetResidentZoneListPacket = 0x155;
        public const ushort CSResidentFireNuonsArrowPacket = 0x120;
        public const ushort CSUseBlessUthstinInitStatsPacket = 0x099;
        public const ushort CSUseBlessUthstinExtendMaxStatsPacket = 0x0bc;
        public const ushort CSBlessUthstinUseApplyStatsItemPacket = 0x17c;
        public const ushort CSBlessUthstinApplyStatsPacket = 0x0fe;
        public const ushort CSEventCenterAddAttendancePacket = 0x0b5;
        public const ushort CSRequestGameEventInfoPacket = 0x185;
        public const ushort CSUnknown0x0cbPacket = 0x0cb;
        public const ushort CSUnknown0x0aPacket = 0x00a;
        public const ushort CSChangeMateNamePacket = 0x090;
        public const ushort CSSendNationMemberCountListPacket = 0x07e;
        public const ushort CSNationSendExpeditionImmigrationAcceptRejectPacket = 0x056;
        public const ushort CSSendExpeditionImmigrationListPacket = 0x02a;
        public const ushort CSSendRelationFriendPacket = 0x180;
        public const ushort CSSendRelationVotePacket = 0x07a;
        public const ushort CSSendNationInfoSetPacket = 0x0db;
        public const ushort CSRankCharacterPacket = 0x18e;
        public const ushort CSRankSnapshotPacket = 0x131;
        public const ushort CSHeroRequestRankDataPacket = 0x05a;
        public const ushort CSGetRankerInformationPacket = 0x15f;
        public const ushort CSRequestRankerAppearancePacket = 0x12c;
        public const ushort CSRequestSecondPassKeyTablesPacket = 0x04f;
        public const ushort CSCreateSecondPassPacket = 0x135;
        public const ushort CSChangeSecondPassPacket = 0x10f;
        public const ushort CSClearSecondPassPacket = 0x078;
        public const ushort CSCheckSecondPassPacket = 0x0ce;
        public const ushort CSReplyImprisonOrTrialPacket = 0x18c;
        public const ushort CSSkipFinalStatementPacket = 0x156;
        public const ushort CSReplyInviteJuryPacket = 0x073;
        public const ushort CSJurySummonedPacket = 0x070;
        public const ushort CSJuryEndTestimonyPacket = 0x029;
        public const ushort CSCancelTrialPacket = 0x14a;
        public const ushort CSJurySentencePacket = 0x145;
        public const ushort CSReportCrimePacket = 0x16b;
        public const ushort CSRequestJuryWaitingNumberPacket = 0x0d7;
        public const ushort CSRequestSetBountyPacket = 0x0df;
        public const ushort CSUpdateBountyPacket = 0x141;
        public const ushort CSTrialReportBadUserPacket = 0x01d;
        public const ushort CSTrialRequestBadUserListPacket = 0x0ac;
        public const ushort CSsUnknown0x146Packet = 0x146;
        public const ushort CSSendUserMusicPacket = 0x026;
        public const ushort CSSaveUserMusicNotesPacket = 0x0b9;
        public const ushort CSRequestMusicNotesPacket = 0x074;
        public const ushort CSPauseUserMusicPacket = 0x052;
        public const ushort CSUnknown0x5ePacket = 0x05e;
        public const ushort CSBagHandleSelectiveItemsPacket = 0x07b;
        public const ushort CSSkillControllerStatePacket = 0x09b;
        public const ushort CSMountMatePacket = 0x15c;
        public const ushort CSLeaveWorldPacket = 0x0eb;
        public const ushort CSCancelLeaveWorldPacket = 0x065;
        public const ushort CSIdleStatusPacket = 0x1a2;
        public const ushort CSChangeClientNpcTargetPacket = 0x0ec;
        public const ushort CSCompletedCinemaPacket = 0x063;
        public const ushort CSCheckDemoModePacket = 0x0f6;
        public const ushort CSDemoCharResetPacket = 0x14c;
        public const ushort CSConsoleCmdUsedPacket = 0x04e;
        public const ushort CSEditorGameModePacket = 0x13e;
        public const ushort CSInteractGimmickPacket = 0x01f;
        public const ushort CSWorldRaycastingPacket = 0x06e;
        public const ushort CSOpenExpeditionImmigrationRequestPacket = 0x0bd;
        public const ushort CSNationGetNationNamePacket = 0x071;
        public const ushort CSRefreshInCharacterListPacket = 0x0c9;
        public const ushort CSDeleteCharacterPacket = 0x0c1;
        public const ushort CSCancelCharacterDeletePacket = 0x0fb;
        public const ushort CSSelectCharacterPacket = 0x0e9;
        public const ushort CSCharacterConnectionRestrictPacket = 0x03f;
        public const ushort CSNotifyInGamePacket = 0x097;
        public const ushort CSNotifyInGameCompletedPacket = 0x1ad;
        public const ushort CSChangeTargetPacket = 0x010;
        public const ushort CSUnknown0x8bPacket = 0x08b;
        public const ushort CSGetSiegeAuctionBidCurrencyPacket = 0x0bf;
        public const ushort CSResurrectCharacterPacket = 0x14f;
        public const ushort CSCriminalLockedPacket = 0x157;
        public const ushort CSExpressEmotionPacket = 0x03b;
        public const ushort CSUnhangPacket = 0x046;
        public const ushort CSChangeAppellationPacket = 0x042;
        public const ushort CSStartedCinemaPacket = 0x0f3;
        public const ushort CSBroadcastVisualOptionPacket = 0x0b2;
        public const ushort CSBroadcastOpenEquipInfoPacket = 0x10b;
        public const ushort CSRestrictCheckPacket = 0x072;
        public const ushort CSIcsMenuListRequestPacket = 0x00e;
        public const ushort CSIcsGoodsListRequestPacket = 0x12e;
        public const ushort CSIcsBuyGoodRequestPacket = 0x05b;
        public const ushort CSPremiumServiceMsgPacket = 0x037;
        public const ushort CSProtectSensitiveOperationPacket = 0x01e;
        public const ushort CSCancelSensitiveOperationVerifyPacket = 0x188;
        //public const ushort CSAntibotDataPacket = 0x; //неизвестный опкод
        public const ushort CSBuyAAPointPacket = 0x144;
        public const ushort CSRequestTencentFatigueInfoPacket = 0x13a;
        public const ushort CSPremiumServiceListPacket = 0x0af;
        public const ushort CSRequestSysInstanceIndexPacket = 0x123;
        public const ushort CSQuitResponsePacket = 0x181;
        public const ushort CSSecurityReportPacket = 0x022;
        public const ushort CSEnprotectStubCallResponsePacket = 0x0e1;
        public const ushort CSRepresentCharacterPacket = 0x100;
        public const ushort CSPacketUnknown0x0aaPacket = 0x0aa;
        public const ushort CSPacketUnknown0x166Packet = 0x166;
        public const ushort CSCreateCharacterPacket = 0x179;
        public const ushort CSEditCharacterPacket = 0x082;
        public const ushort CSSpawnCharacterPacket = 0x031;
        public const ushort CSTeleportEndedPacket = 0x15a;
        public const ushort CSNotifySubZonePacket = 0x1ab;
        public const ushort CSSaveTutorialPacket = 0x023;
        public const ushort CSRequestUIDataPacket = 0x050;
        public const ushort CSSaveUIDataPacket = 0x0b3;
        public const ushort CSBeautyshopDataPacket = 0x0d4;
        public const ushort CSDominionUpdateTaxratePacket = 0x13b;
        public const ushort CSDominionUpdateNationalTaxratePacket = 0x002;
        public const ushort CSRequestCharacterBriefPacket = 0x115;
        public const ushort CSExpeditionCreatePacket = 0x01b;
        public const ushort CSExpeditionChangeRolePolicyPacket = 0x08d;
        public const ushort CSExpeditionMemberRolePacket = 0x0b8;
        public const ushort CSExpeditionChangeOwnerPacket = 0x02b;
        public const ushort CSChangeNationOwnerPacket = 0x112;
        public const ushort CSRenameFactionPacket = 0x13c;
        public const ushort CSExpeditionDismissPacket = 0x0cd;
        public const ushort CSExpeditionInvitePacket = 0x0f7;
        public const ushort CSExpeditionLeavePacket = 0x152;
        public const ushort CSExpeditionKickPacket = 0x178;
        public const ushort CSExpeditionBeginnerJoinPacket = 0x132;
        public const ushort CSDeclareExpeditionWarPacket = 0x125;
        public const ushort CSFactionGetDeclarationMoneyPacket = 0x121;
        public const ushort CSUnknown0x0a3Packet = 0x0a3;
        public const ushort CSFactionGetExpeditionWarHistoryPacket = 0x093;
        public const ushort CSFactionCancelProtectionPacket = 0x018;
        public const ushort CSFactionImmigrationInvitePacket = 0x0f9;
        public const ushort CSFactionImmigrationInviteReplyPacket = 0x0ea;
        public const ushort CSFactionImmigrateToOriginPacket = 0x0a0;
        public const ushort CSFactionKickToOriginPacket = 0x003;
        public const ushort CSFactionMobilizationOrderPacket = 0x14b;
        public const ushort CSFactionCheckExpeditionExpNextDayPacket = 0x187;
        public const ushort CSFactionSetExpeditionLevelUpPacket = 0x0fa;
        public const ushort CSFactionSetExpeditionMotdPacket = 0x0f2;
        public const ushort CSFactionSetMyExpeditionInterestPacket = 0x095;
        public const ushort CSUnknown0x60Packet = 0x060;
        public const ushort CSExpeditionReplyInvitationPacket = 0x104;
        public const ushort CSFamilyInviteMemberPacket = 0x0a9;
        public const ushort CSFamilyLeavePacket = 0x12f;
        public const ushort CSFamilyKickPacket = 0x0dc;
        public const ushort CSFamilyChangeTitlePacket = 0x0a5;
        public const ushort CSFamilyChangeOwnerPacket = 0x09f;
        public const ushort CSFamilySetNamePacket = 0x177;
        public const ushort CSFamilySetContentPacket = 0x061;
        public const ushort CSFamilyOpenIncreaseMemberPacketPacket = 0x015;
        public const ushort CSFamilyChangeMemberRolePacket = 0x1a3;
        public const ushort CSFamilyReplyInvitationPacket = 0x006;
        public const ushort CSAddFriendPacket = 0x106;
        public const ushort CSDeleteFriendPacket = 0x021;
        public const ushort CSAddBlockedUserPacket = 0x182;
        public const ushort CSDeleteBlockedUserPacket = 0x15e;
        public const ushort CSInviteAreaToTeamPacket = 0x079;
        public const ushort CSInviteToTeamPacket = 0x10a;
        public const ushort CSReplyToJoinTeamPacket = 0x0fd;
        public const ushort CSLeaveTeamPacket = 0x113;
        public const ushort CSKickTeamMemberPacket = 0x014;
        public const ushort CSMakeTeamOwnerPacket = 0x0bb;
        public const ushort CSConvertToRaidteamPacket = 0x12d;
        public const ushort CSMoveTeamMemberPacket = 0x0d9;
        public const ushort CSDismissTeamPacket = 0x0ad;
        public const ushort CSSetTeamMemberRolePacket = 0x054;
        public const ushort CSSetOverHeadMarkerPacket = 0x0c7;
        public const ushort CSAskRiskyTeamActionPacket = 0x040;
        public const ushort CSTeamAcceptHandOverOwnerPacket = 0x005;
        public const ushort CSTeamAcceptOwnerOfferPacket = 0x0b6;
        public const ushort CSChangeLootingRulePacket = 0x127;
        public const ushort CSRenameCharacterPacket = 0x142;
        public const ushort CSUpdateActionSlotPacket = 0x0ca;
        public const ushort CSUsePortalPacket = 0x094;
        public const ushort CSUpgradeExpertLimitPacket = 0x0d1;
        public const ushort CSDowngradeExpertLimitPacket = 0x192;
        public const ushort CSExpandExpertPacket = 0x162;
        public const ushort CSEnterSysInstancePacket = 0x114;
        public const ushort CSEndPortalInteractionPacket = 0x028;
        public const ushort CSCreateShipyardPacket = 0x17f;
        public const ushort CSCreateHousePacket = 0x10e;
        public const ushort CSLeaveBeautyshopPacket = 0x09c;
        public const ushort CSConstructHouseTaxPacket = 0x09d;
        public const ushort CSChangeHouseNamePacket = 0x147;
        public const ushort CSChangeHousePermissionPacket = 0x0fc;
        public const ushort CSRequestHouseTaxPacket = 0x0e0;
        public const ushort CSPerpayHouseTaxPacket = 0x103;
        public const ushort CSAllowRecoverPacket = 0x0ff;
        public const ushort CSSellHouseCancelPacket = 0x150;
        public const ushort CSDecorateHousePacket = 0x045;
        public const ushort CSSellHousePacket = 0x116;
        public const ushort CSBuyHousePacket = 0x020;
        public const ushort CSRotateHousePacket = 0x0f8;
        public const ushort CSRemoveMatePacket = 0x13d;
        public const ushort CSChangeMateTargetPacket = 0x1a7;
        public const ushort CSChangeMateUserStatePacket = 0x17b;
        public const ushort CSSpawnSlavePacket = 0x11c;
        public const ushort CSDespawnSlavePacket = 0x195;
        public const ushort CSDestroySlavePacket = 0x151;
        public const ushort CSBindSlavePacket = 0x039;
        public const ushort CSRemoveAllFieldSlavesPacket = 0x03d;
        public const ushort CSBoardingTransferPacket = 0x154;
        public const ushort CSTurretStatePacket = 0x03e;
        public const ushort CSCreateSkillControllerPacket = 0x126;
        public const ushort CSJoinTrialAudiencePacket = 0x199;
        public const ushort CSLeaveTrialAudiencePacket = 0x0cf;
        public const ushort CSUnMountMatePacket = 0x175;
        public const ushort CSUnbondDoodadPacket = 0x048;
        public const ushort CSInstanceLoadedPacket = 0x11f;
        public const ushort CSApplyToInstantGamePacket = 0x1a4;
        public const ushort CSCancelInstantGamePacket = 0x130;
        public const ushort CSJoinInstantGamePacket = 0x03a;
        public const ushort CSEnteredInstantGameWorldPacket = 0x129;
        public const ushort CSLeaveInstantGamePacket = 0x051;
        public const ushort CSPickBuffInstantGamePacket = 0x16d;
        public const ushort CSBattlefieldPickshipPacket = 0x0ae;
        public const ushort CSRequestPermissionToPlayCinemaForDirectingModePacket = 0x136;
        public const ushort CSStartQuestContextPacket = 0x10c;
        public const ushort CSCompleteQuestContextPacket = 0x083;
        public const ushort CSDropQuestContextPacket = 0x0c3;
        public const ushort CSQuestTalkMadePacket = 0x168;
        public const ushort CSQuestStartWithParamPacket = 0x036;
        public const ushort CSTryQuestCompleteAsLetItDonePacket = 0x013;
        public const ushort CSRestartMainQuestPacket = 0x001;
        public const ushort CSLearnSkillPacket = 0x085;
        public const ushort CSLearnBuffPacket = 0x19a;
        public const ushort CSResetSkillsPacket = 0x12a;
        public const ushort CSSwapAbilityPacket = 0x16f;
        public const ushort CSSelectHighAbilityPacket = 0x0d5;
        public const ushort CSUnknown0x18dPacket = 0x18d;
        public const ushort CSRemoveBuffPacket = 0x19d;
        public const ushort CSStopCastingPacket = 0x096;
        public const ushort CSDeletePortalPacket = 0x02c;
        public const ushort CSIndunDirectTelPacket = 0x004;
        public const ushort CSSetForceAttackPacket = 0x06c;
        public const ushort CSStartSkillPacket = 0x160;
        public const ushort CSUnknown0x122Packet = 0x122;
        public const ushort CSStopLootingPacket = 0x019;
        public const ushort CSCreateDoodadPacket = 0x119;
        public const ushort CSNaviTeleportPacket = 0x196;
        public const ushort CSNaviOpenPortalPacket = 0x128;
        public const ushort CSNaviOpenBountyPacket = 0x092;
        public const ushort CSSetLogicDoodadPacket = 0x16e;
        public const ushort CSCleanupLogicLinkPacket = 0x081;
        public const ushort CSSelectInteractionExPacket = 0x1aa;
        public const ushort CSChangeDoodadDataPacket = 0x0e8;
        public const ushort CSBuyItemsPacket = 0x14d;
        public const ushort CSUnknown0x59Packet = 0x059;
        public const ushort CSUnknown0x1a5Packet = 0x1a5;
        public const ushort CSUnknown0x30Packet = 0x030;
        public const ushort CSUnknown0x5dPacket = 0x05d;
        public const ushort CSInteractNPCPacket = 0x066;
        public const ushort CSInteractNPCEndPacket = 0x0b1;
        public const ushort CSBeautyshopBypassPacket = 0x19f;
        public const ushort CSSpecialtyRatioPacket = 0x055;
        public const ushort CSUnknown0x17Packet = 0x017;
        public const ushort CSJoinUserChatChannelPacket = 0x169;
        public const ushort CSLeaveChatChannelPacket = 0x053;
        public const ushort CSSendChatMessagePacket = 0x167;
        public const ushort CSRollDicePacket = 0x034;
        public const ushort CSSendMailPacket = 0x143;
        public const ushort CSListMailPacket = 0x171;
        public const ushort CSListMailContinuePacket = 0x01a;
        public const ushort CSReadMailPacket = 0x158;
        public const ushort CSTakeAttachmentMoneyPacket = 0x102;
        public const ushort CSTakeAttachmentSequentiallyPacket = 0x0f5;
        public const ushort CSPayChargeMoneyPacket = 0x0b4;
        public const ushort CSDeleteMailPacket = 0x062;
        public const ushort CSReportSpamPacket = 0x064;
        public const ushort CSReturnMailPacket = 0x0b7;
        public const ushort CSTakeAllAttachmentItemPacket = 0x176;
        public const ushort CSTakeAttachmentItemPacket = 0x197;
        public const ushort CSActiveWeaponChangedPacket = 0x0a2;
        public const ushort CSUnknown0x0d8Packet = 0x0d8;
        public const ushort CSRequestExpandAbilitySetSlotPacket = 0x0c2;
        public const ushort CSSaveAbilitySetPacket = 0x0e3;
        public const ushort CSDeleteAbilitySetPacket = 0x1a6;
        public const ushort CSRepairSlaveItemsPacket = 0x016;
        public const ushort CSRepairPetItemsPacket = 0x137;
        public const ushort CSFactionIssuanceOfMobilizationOrderPacket = 0x06b;
        public const ushort CSGetExpeditionMyRecruitmentsPacket = 0x148;
        public const ushort CSExpeditionRecruitmentAddPacket = 0x0e5;
        public const ushort CSExpeditionRecruitmentDeletePacket = 0x11e;
        public const ushort CSGetExpeditionApplicantsPacket = 0x0e7;
        public const ushort CSExpeditionApplicantAddPacket = 0x18a;
        public const ushort CSExpeditionApplicantDeletePacket = 0x139;
        public const ushort CSExpeditionApplicantAcceptPacket = 0x0c5;
        public const ushort CSExpeditionApplicantRejectPacket = 0x0e6;
        public const ushort CSExpeditionSummonPacket = 0x12b;
        public const ushort CSExpeditionSummonReplyPacket = 0x047;
        public const ushort CSInstantTimePacket = 0x033;
        public const ushort CSSetHouseAllowRecoverPacket = 0x1ae;
        public const ushort CSRefreshBotCheckInfoPacket = 0x032;
        public const ushort CSAnswerBotCheckPacket = 0x091;
        public const ushort CSChangeSlaveNamePacket = 0x02d;
        public const ushort CSUseTeleportPacket = 0x07f;
        public const ushort CSAuctionPostPacket = 0x118;
        public const ushort CSAuctionSearchPacket = 0x075;
        public const ushort CSAuctionMyBidListPacket = 0x067;
        public const ushort CSAuctionLowestPricePacket = 0x024;
        public const ushort CSAuctionSearchSoldRecordPacket = 0x025;
        public const ushort CSAuctionCancelPacket = 0x161;
        public const ushort CSAuctionBidPacket = 0x193;
        public const ushort CSExecuteCraftPacket = 0x149;
        public const ushort CSSetLpManageCharacterPacket = 0x098;
        public const ushort CSSetCraftingPayPacket = 0x0d3;
        public const ushort CSDestroyItemPacket = 0x0ef;
        public const ushort CSSplitBagItemPacket = 0x172;
        public const ushort CSSwapItemsPacket = 0x124;
        public const ushort CSSplitCofferItemPacket = 0x0de;
        public const ushort CSSwapCofferItemsPacket = 0x11d;
        public const ushort CSExpandSlotsPacket = 0x0d2;
        public const ushort CSDepositMoneyPacket = 0x058;
        public const ushort CSWithdrawMoneyPacket = 0x04b;
        public const ushort CSItemSecurePacket = 0x17e;
        public const ushort CSItemUnsecurePacket = 0x16c;
        public const ushort CSEquipmentsSecurePacket = 0x08f;
        public const ushort CSEquipmentsUnsecurePacket = 0x08a;
        public const ushort CSRepairSingleEquipmentPacket = 0x10d;
        public const ushort CSRepairAllEquipmentsPacket = 0x19e;
        public const ushort CSChangeAutoUseAaPointPacket = 0x01c;
        public const ushort CSThisTimeUnpackPacket = 0x0cc;
        //public const ushort CSTakeScheduleItemPacket = 0x0; //неизвестный опкод
        public const ushort CSChangeMateEquipmentPacket = 0x00f;
        public const ushort CSChangeSlaveEquipmentPacket = 0x117;
        public const ushort CSLoginUccItemsPacket = 0x035;
        public const ushort CSLootOpenBagPacket = 0x159;
        public const ushort CSLootItemPacket = 0x164;
        public const ushort CSLootCloseBagPacket = 0x110;
        public const ushort CSLootDicePacket = 0x049;
        public const ushort CSSellBackpackGoodsPacket = 0x153;
        public const ushort CSSellItemsPacket = 0x105;
        public const ushort CSListSoldItemPacket = 0x027;
        public const ushort CSSpecialtyCurrentLoadPacket = 0x044;
        public const ushort CSStartTradePacket = 0x09a;
        public const ushort CSCanStartTradePacket = 0x0d0;
        public const ushort CSCannotStartTradePacket = 0x077;
        public const ushort CSCancelTradePacket = 0x011;
        //public const ushort CSPutupItemPacket = 0x0c4;
        public const ushort CSPutupTradeItemPacket = 0x0c4;
        public const ushort CSTakedownItemPacket = 0x0c8;
        public const ushort CSTradeLockPacket = 0x19c;
        public const ushort CSTradeOkPacket = 0x076;
        //public const ushort CSPutupMoneyPacket = 0x15b;
        public const ushort CSPutupTradeMoneyPacket = 0x15b;
        public const ushort CSReportSpammerPacket = 0x087;

        // эти опкоды для версии 3030 отсутствуют в x2game.dll
        public const ushort CSAcceptCheatQuestContextPacket = 0x0ffff;
        public const ushort CSAllowHousingRecoverPacket = 0x0ffff;
        public const ushort CSBidAuctionPacket = 0x0ffff;
        public const ushort CSBuyCoinItemPacket = 0x0ffff;
        public const ushort CSBuyPriestBuffPacket = 0x0ffff;
        public const ushort CSBuySpecialtyItemPacket = 0x0ffff;
        public const ushort CSCancelAuctionPacket = 0x0ffff;
        public const ushort CSChangeDoodadPhasePacket = 0x0ffff;
        public const ushort CSChangeExpeditionMemberRolePacket = 0x0ffff;
        public const ushort CSChangeExpeditionOwnerPacket = 0x0ffff;
        public const ushort CSChangeExpeditionRolePolicyPacket = 0x0ffff;
        public const ushort CSChangeExpeditionSponsorPacket = 0x0ffff;
        public const ushort CSChangeHousePayPacket = 0x0ffff;
        public const ushort CSChangeItemLookPacket = 0x0ffff;
        public const ushort CSChangeSlaveTargetPacket = 0x0ffff;
        public const ushort CSCharDetailPacket = 0x0ffff;
        public const ushort CSConvertToRaidTeamPacket = 0x0ffff;
        public const ushort CSCreateExpeditionPacket = 0x0ffff;
        public const ushort CSDiscardSlavePacket = 0x0ffff;
        public const ushort CSDismissExpeditionPacket = 0x0ffff;
        public const ushort CSFactionDeclareHostilePacket = 0x0ffff;
        public const ushort CSHangPacket = 0x0ffff;
        public const ushort CSICSBuyGoodPacket = 0x0ffff;
        public const ushort CSICSGoodsListPacket = 0x0ffff;
        public const ushort CSICSMenuListPacket = 0x0ffff;
        public const ushort CSICSMoneyRequestPacket = 0x0ffff;
        public const ushort CSInviteToExpeditionPacket = 0x0ffff;
        public const ushort CSJuryVerdictPacket = 0x0ffff;
        public const ushort CSKickFromExpeditionPacket = 0x0ffff;
        public const ushort CSLeaveExpeditionPacket = 0x0ffff;
        public const ushort CSListCharacterPacket = 0x0ffff;
        public const ushort CSListSpecialtyGoodsPacket = 0x0ffff;
        public const ushort CSQuestStartWithPacket = 0x0ffff;
        public const ushort CSRenameExpeditionPacket = 0x0ffff;
        public const ushort CSReplyExpeditionInvitationPacket = 0x0ffff;
        public const ushort CSRequestCharBriefPacket = 0x0ffff;
        public const ushort CSRequestSecondPasswordKeyTablesPacket = 0x0ffff;
        public const ushort CSResetQuestContextPacket = 0x0ffff;
        public const ushort CSSaveDoodadUccStringPacket = 0x0ffff;
        public const ushort CSSearchListPacket = 0x0ffff;
        public const ushort CSSetTeamOfficerPacket = 0x0ffff;
        public const ushort CSSetupSecondPassword = 0x0ffff;
        public const ushort CSSpecialtyRecordLoadPacket = 0x0ffff;
        public const ushort CSStartInteractionPacket = 0x0ffff;
        public const ushort CSTakedownTradeItemPacket = 0x0ffff;
        public const ushort CSThisTimeUnpackItemPacket = 0x0ffff;
        public const ushort CSUpdateDominionTaxRatePacket = 0x0ffff;
        public const ushort CSUpdateNationalTaxRatePacket = 0x0ffff;
    }
}