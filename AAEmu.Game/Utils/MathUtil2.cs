﻿using System;
using System.Numerics;

using AAEmu.Game.Models.Game.Units;
using AAEmu.Game.Models.Game.World;

namespace AAEmu.Game.Utils
{
    public static class MathUtil2
    {
        private const double Pi = 3.14159;
        private const double Pi2 = 3.14159 * 2;
        private const double Pi05 = 3.14159 * 0.5;

        // Return degree value of object 2 to the horizontal line with object 1 being the origin
        public static double CalculateAngleFrom(GameObject obj1, GameObject obj2)
        {
            return CalculateAngleFrom(obj1.Position.X, obj1.Position.Y, obj2.Position.X, obj2.Position.Y);
        }

        // Return degree value of object 2 to the horizontal line with object 1 being the origin
        public static double CalculateAngleFrom(float obj1X, float obj1Y, float obj2X, float obj2Y)
        {
            var angleTarget = RadianToDegree(Math.Atan2(obj2Y - obj1Y, obj2X - obj1X));
            if (angleTarget < 0)
            {
                angleTarget += 360;
            }

            return angleTarget;
        }

        // Return degree value of object 2 to the horizontal line with object 1 being the origin
        public static double CalculateAngleFrom(Vector3 obj1, Vector3 obj2)
        {
            var angleTarget = RadianToDegree(Math.Atan2(obj2.Y - obj1.Y, obj2.X - obj1.X));
            if (angleTarget < 0)
            {
                angleTarget += 360;
            }

            return angleTarget;
        }

        public static double RadianToDegree(double angle)
        {
            return angle * 57.29578f;     //180/Пи — перевод в градусы (180.0 / Math.PI);
        }

        public static double DegreeToRadian(double angle)
        {
            return angle * 0.0174532924f; //Пи/180 — перевод в радианы (Math.PI / 180);
        }

        public static double ConvertDirectionToDegree(sbyte direction)
        {
            var angle = direction * 2.8125 + 90; // 2.8125 = 360f / 128
            if (angle < 0)
            {
                angle += 360;
            }

            return angle;
        }

        public static sbyte ConvertDegreeToDirection(double degree)
        {
            if (degree < 0)
            {
                degree = 360 + degree;
            }

            degree -= 90;
            var res = (sbyte)(degree / 2.8125);         // 2.8125 = 360f / 128;
            if (res > 85)
            {
                res = (sbyte)((degree - 360) / 2.8125); // 2.8125 = 360f / 128;
            }

            return res;
        }

        public static sbyte ConvertDegreeToDoodadDirection(double degree)
        {
            while (degree < 0f)
            {
                degree += 360f;
            }

            if (degree > 90f && degree <= 180f)
            {
                return (sbyte)(((degree - 90f) / 90f * 37f + 90f) * -1);
            }

            if (degree > 180f)
            {
                return (sbyte)(((degree - 270f) / 90f * 37f - 90f) * -1);
            }
            // When range is between -90 and 90, no rotation scaling is applied for doodads
            return (sbyte)(degree * -1);
        }
        public static bool IsFront(GameObject obj1, GameObject obj2)
        {
            var degree = CalculateAngleFrom(obj1, obj2);
            var degree2 = ConvertDirectionToDegree(obj2.Position.RotationZ);
            var diff = Math.Abs(degree - degree2);

            if (diff >= 90 && diff <= 270)
            {
                return true;
            }

            return false;
        }

        //public static double ConvertDirectionToRadian(sbyte direction)
        //{
        //    return ConvertDirectionToDegree(direction) * Math.PI / 180.0;
        //}

        /// <summary>
        /// Convert Direction To Radian
        /// </summary>
        /// <param name="direction">0..127</param>
        /// <returns>angle in radian</returns>
        public static double ConvertDirectionToRadian(sbyte direction)
        {
            return direction * 0.04947389975474138663881739572689; // 0.04947389975474138663881739572689 = 0.0078740157f * 6.283185307179586f;
        }

        /// <summary>
        /// Add Distance To Front
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="rotZ">0..127</param>
        /// <returns></returns>
        public static (float, float) AddDistanceToFront(float distance, float x, float y, sbyte rotZ)
        {
            var rad = ConvertDirectionToRadian(rotZ);
            var newX = distance * (float)Math.Cos(rad) + x;
            var newY = distance * (float)Math.Sin(rad) + y;
            return (newX, newY);
        }

        public static (float, float) AddDistanceToRight(float distance, float x, float y, sbyte rotZ)
        {
            var rad = ConvertDirectionToRadian(rotZ) - 1.5707963267948966192313216916398; // 1.5707963267948966192313216916398 = Math.PI / 2;
            var newX = distance * (float)Math.Cos(rad) + x;
            var newY = distance * (float)Math.Sin(rad) + y;
            return (newX, newY);
        }

        //public static sbyte ConvertRadianToDirection(double radian) // TODO float zRot
        //{
        //    var degree = RadianToDegree(radian);
        //    if (degree < 0)
        //        degree = 360 + degree;
        //    var res = (sbyte)(degree / (360f / 128));
        //    if (res > 85)
        //        res = (sbyte)((degree - 360) / (360f / 128));
        //    return res;
        //}

        public static sbyte ConvertRadianToDirection(double radian)
        {
            if (radian < 0)
            {
                radian = Math.PI * 2 + radian;
            }

            var res = (sbyte)(radian / 0.00125318852063819041660096232427);                       // 0.00125318852063819041660096232427 = 0.0078740157f / 6.283185307179586f
            if (res > 85)
            {
                res = (sbyte)((radian - 6.283185307179586) / 0.00125318852063819041660096232427); // 0.00125318852063819041660096232427 = 0.0078740157 / 6.283185307179586
            }

            return res;
        }

        public static float CalculateDistance(Point loc, Point loc2, bool includeZAxis = false)
        {
            double dx = loc.X - loc2.X;
            double dy = loc.Y - loc2.Y;

            if (includeZAxis)
            {
                double dz = loc.Z - loc2.Z;
                return (float)Math.Sqrt(dx * dx + dy * dy + dz * dz);
            }

            return (float)Math.Sqrt(dx * dx + dy * dy);
        }
        public static float GetDistance(Vector3 v1, Vector3 v2, bool point3d = false)
        {
            return point3d
                ?
                MathF.Sqrt(MathF.Pow(v1.X - v2.X, 2) + MathF.Pow(v1.Y - v2.Y, 2) + MathF.Pow(v1.Y - v2.Y, 2))
                :
                MathF.Sqrt(MathF.Pow(v1.X - v2.X, 2) + MathF.Pow(v1.Y - v2.Y, 2));
        }
        public static double CalculateDirection(Vector3 obj1, Vector3 obj2)
        {
            var rad = Math.Atan2(obj2.Y - obj1.Y, obj2.X - obj1.X);

            return rad;
        }
        public static ushort ConvertDegreeToDirectionShort(double degree)
        {
            var rotateModifier = 1;

            if (degree < 0)
            {
                rotateModifier = -1;   // remember the sign of the angle
                degree = 360 + degree; // working with positive angles
            }
            //if (degree > 180)
            //{
            //    degree = 360 - degree;  // we work only with angles up to 180 degrees
            //}
            degree -= 90; // 12 o'clock == 0°
            if (degree < 0)
            {
                rotateModifier = -1;   // remember the sign of the angle
                                       //    degree = 360 + degree; // working with positive angles
            }
            double direction = 0f;
            if (Math.Abs(degree) > 135 && Math.Abs(degree) < 225)
            {
                direction = Math.Abs(degree) * 205.6814814814815; //182.0444444444444;
                direction = Math.Clamp(direction, -32767d, 32767d);
            }
            else if (Math.Abs(degree) > 90 && Math.Abs(degree) <= 135 || Math.Abs(degree) < 270 && Math.Abs(degree) >= 225)
            {
                direction = Math.Abs(degree) * 205.6814814814815;
            }
            else if (Math.Abs(degree) >= 0 && Math.Abs(degree) <= 90 || Math.Abs(degree) <= 360 && Math.Abs(degree) >= 270)
            {
                direction = Math.Abs(degree) * 252.9777777777778;
            }
            //_log.Warn("Degree={0}, Direction={1}", degree, direction);

            //direction = Math.Abs(degree) * 252.9777777777778;
            //direction = Math.Clamp(direction, -32767d, 32767d);

            return (ushort)(direction * rotateModifier);
        }

        public static double GetAngle(Vector3 origin, Vector3 target)
        {
            var vector2 = target - origin;
            if (vector2 != Vector3.Zero)
            {
                vector2 = Vector3.Normalize(vector2);
            }
            var vector1 = new Vector3(1, 0, 0); // 12 o'clock == 0°, assuming that y goes from bottom to top

            var spv = Vector3.Dot(vector1, vector2);
            var mv1 = Math.Sqrt(vector1.X * vector1.X + vector1.Y * vector1.Y);
            var mv2 = Math.Sqrt(vector2.X * vector2.X + vector2.Y * vector2.Y);
            var ab = spv / (mv1 * mv2);
            var angleInRadians = Math.Acos(ab);
            //var angleInRadians = Math.Atan2(vector2.Y, vector2.X) - Math.Atan2(vector1.Y, vector1.X);

            return angleInRadians;
        }

        public static short UpdateHeading(Vector3 point, Vector3 target)
        {
            var rad = Math.Atan2(target.Y - point.Y, target.X - point.X);
            rad -= Pi / 2; // 12 o'clock == 0°
            var heading = (short)(rad * 32767 / Pi);

            return heading;
        }
        public static ushort UpdateHeading(double degree, bool radian = false)
        {
            ushort heading;
            if (radian)
            {
                degree -= Pi / 2; // 12 o'clock == 0°
                heading = (ushort)(degree * 32767 / Pi);
            }
            else
            {
                degree -= 90; // 12 o'clock == 0°
                heading = (ushort)(degree * 32767 / 180);
            }
            //    52 - 0.00371041
            //     1 - 0.000071353925
            // 65536 - 4.6762582646153846153846153846154

            return heading;
        }

        public static sbyte ConvertDegreeToDirection2(double degree)
        {
            var rotateModifier = 1;
            if (degree < 0)
            {
                //rotateModifier = -1;   // remember the sign of the angle
                degree = 360 + degree; // working with positive angles
            }
            degree -= 90; // 12 o'clock == 0°
            //if (degree > 180)
            //{
            //    degree = 360 - degree;  // we work only with angles up to 180 degrees
            //}
            var res = (sbyte)(degree / (180f / 127) * rotateModifier);
            //if (res > 85)
            //    res = (sbyte)((degree - 360) / (180f / 127) * rotateModifier);
            return res;
        }
        public static short UpdateHeading(Unit obj, Vector3 target)
        {
            return (short)(Math.Atan2(target.Y - obj.Position.Y, target.X - obj.Position.X) * 32768 / Pi);
        }

        // ===========================================================================================================
        /*
         create a unit quaternion rotating by axis angle representation
        */
        public static Quaternion unitFromAxisAngle(Vector3 axis, float angle)
        {
            var v = Vector3.Normalize(axis);
            var halfAngle = angle * 0.5f;
            var sinA = (float)Math.Sin(halfAngle);
            var quaternion = new Quaternion(v.X * sinA, v.Y * sinA, v.Z * sinA, (float)Math.Cos(halfAngle));
            return quaternion;
        }
        //-----------------------------------
        /*
          convert a quaternion to axis angle representation, 
          preserve the axis direction and angle from -PI to +PI
        */
        public static (Vector3, float) toAxisAngle(float x, float y, float z, float w = 1.0f)
        {
            Vector3 axis;
            float angle;

            var vl = (float)Math.Sqrt(x * x + y * y + z * z);

            if (vl > 0.99993801 || vl < 0.000062000123)
            {
                axis = new Vector3(0, 0, 0);
                angle = 1.0f;
            }
            else
            {
                var ivl = 1.0f / vl;
                axis = new Vector3(x * ivl, y * ivl, z * ivl);
                if (w < 0)
                {
                    angle = 2.0f * (float)Math.Atan2(-vl, -w); // -PI, 0
                }
                else
                {
                    angle = 2.0f * (float)Math.Atan2(vl, w);   //   0, PI
                }
            }

            return (axis, angle);
        }
        //-----------------------------------
        /*
        С помощью "Shortest arc" можно ориентировать ракету в направлении полета, причем ее повороты будут выглядеть естественно
        (разворот по кратчайшей дуге). Алгоритм очень прост, на каждом шаге берем предыдущий вектор направления. Строим "Shortest
        arc" от него к текущему направлению и поворачиваем объект на получившийся кватернион. Если мы применим повороты с помощью
        "Shortest arc" при движении по непрерывной кривой (например, по сплайну), мы реализуем очень полезный метод "parallel
        transport frame". Этот метод дает нам как бы ориентацию каната протянутого по этой кривой и минимизирует скручивание
        "twist" каната. Это особенно полезно для создания объектов по заданному трафарету и профилю кривой.
         Для решения задачи инверсной кинематики, когда по заданному направлению надо найти необходимый поворот
        "Shortest arc" придется как нельзя кстати.
        */

        /*
        the shortest arc quaternion that will rotate one vector to another.
        create rotation from -> to, for any length vectors
        */
        public static Quaternion shortestArc(Vector3 from, Vector3 to)
        {
            var q2 = new Quaternion(0, 0, 0, 1);
            var crossV = Vector3.Cross(@from, to);
            var q = new Quaternion(crossV.X, crossV.Y, crossV.Z, Vector3.Dot(@from, to));
            q = Quaternion.Normalize(q);    // if "from" or "to" is not unit, normalize it

            // contains quaternion of "double angle" rotation from to. can be non unit.
            q.W += 1.0f;               // reducing angle to half angle
            if (q.W <= 0.000062000123) // angle close to PI
            {
                if ((@from.Z * @from.Z) > (@from.X * @from.X))
                {
                    q2 = new Quaternion(0, @from.Z, -@from.Y, q.W); // from * vector3(1,0,0) 
                }
                else
                {
                    q2 = new Quaternion(@from.Y, -@from.X, 0, q.W); // from * vector3(0,0,1) 
                }
            }
            q2 = Quaternion.Normalize(q2);
            return q2;
        }

        public static double CalculateDirectionZ(Vector3 obj1, Vector3 obj2)
        {
            var rad = Math.Atan2(obj2.Y - obj1.Y, obj2.Z - obj1.Z);

            return rad;
        }
    }
}
