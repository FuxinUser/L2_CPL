using Core.Define;
using Core.Util;
using MsgStruct;
using System;
using System.Linq;
using static DBService.L1Repository.L1L2MsgDBModel;
using static DBService.L1Repository.L2L1MsgDBModel;
using static MsgStruct.L2L1Snd;

namespace MsgConvert.EntityFactory
{
    public static class L1MsgToL1HistoryEntityFactory
    {
        public static object ConvertL1DBModel(this object message, string msgID)
        {
            object dbModel = null;

            switch (msgID)
            {
                // 接收
                case PlcSysDef.RcvMsgCode.L1301EnCoilCut:
                    dbModel = FromMessage<L1L2_301>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1302WieldRecord:
                    dbModel = FromMessage<L1L2_302>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1303ReqTrackMap:
                    dbModel = FromMessage<L1L2_303>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1305TrackMapEn:
                    dbModel = FromMessage<L1L2_305>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1306TrackMapEx:
                    dbModel = FromMessage<L1L2_306>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1307CoilDismount:
                    dbModel = FromMessage<L1L2_307>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1308CoilWeightScale:
                    dbModel = FromMessage<L1L2_308>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1309EquipMaint:
                    dbModel = FromMessage<L1L2_309>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1310LineFault:
                    dbModel = FromMessage<L1L2_310>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1311ExCoilCut:
                    dbModel = FromMessage<L1L2_311>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1312NewCoilRec:
                    dbModel = FromMessage<L1L2_312>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1313SpdTen:
                    dbModel = FromMessage<L1L2_313>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1315Cdc:
                    dbModel = FromMessage<L1L2_315>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1316Utility:
                    dbModel = FromMessage<L1L2_316>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1317ReturnCoilInfo:
                    dbModel = FromMessage<L1L2_317>(message);
                    break;
                case PlcSysDef.RcvMsgCode.L1318SideTrimmerInfo:
                    dbModel = FromMessage<L1L2_318>(message);
                    break;

                // 發送
                case PlcSysDef.SndMsgCode.L1201Preset:
                    dbModel = FromMessage<L2L1_201>(message);
                    break;
                case PlcSysDef.SndMsgCode.L1202TrackMapL2:
                    dbModel = FromMessage<L2L1_202>(message);
                    break;
                case PlcSysDef.SndMsgCode.L1203SplitID:
                    dbModel = FromMessage<L2L1_203>(message);
                    break;
                case PlcSysDef.SndMsgCode.L1204DelSkID:
                    dbModel = FromMessage<L2L1_204>(message);
                    break;
                case PlcSysDef.SndMsgCode.L1205NewPOR:
                    dbModel = FromMessage<L2L1_205>(message);
                    break;
            };

            return dbModel;
        }

        public static object ConvertL1MsgModel(this object message, string msgID)
        {
            object dbModel = null;

            switch (msgID)
            {
                #region 發送
                case PlcSysDef.SndMsgCode.L1201Preset:
                    dbModel = FromDBModel<Msg_201_Preset>(message);
                    break;
                case PlcSysDef.SndMsgCode.L1202TrackMapL2:
                    dbModel = FromDBModel<Msg_202_TrackMapL2>(message);
                    break;
                case PlcSysDef.SndMsgCode.L1203SplitID:
                    dbModel = FromDBModel<Msg_203_SplitId>(message);
                    break;
                case PlcSysDef.SndMsgCode.L1204DelSkID:
                    dbModel = FromDBModel<Msg_204_DelSkid>(message);
                    break;
                case PlcSysDef.SndMsgCode.L1205NewPOR:
                    dbModel = FromDBModel<Msg_205_NewPORId>(message);
                    break;

                    #endregion
            };

            return dbModel;
        }





        /// <summary>
        /// 報文格式轉換成Tbl , 當作儲存Log之用 (速度平均29ns)
        /// </summary>
        public static T FromMessage<T>(object msgStruct, bool containsDateTime = true)
        {
            //產生ORM實例:
            T orm = Activator.CreateInstance<T>();

            var msgKV = msgStruct.GetType().GetFields().ToDictionary(x => x.Name, x => x.GetValue(msgStruct));
            var ormProps = orm.GetType().GetProperties();

            foreach (var property in ormProps)
            {
                if (msgKV.ContainsKey(property.Name))
                {
                    var value = msgKV[property.Name];
                    if (value.GetType() == typeof(char[]) && property.PropertyType == typeof(string))
                    {
                        property.SetValue(orm, new string(value as char[]));
                    }
                    else if (value.GetType() == typeof(byte[]))
                    {

                        var byteData = value as byte[];
                        property.SetValue(orm, byteData.ToStr());
                    }
                    else
                    {
                        property.SetValue(orm, value);
                    }
                }
            }


            if (containsDateTime)
            {
                var createTime = orm.GetType().GetProperty("CreateTime");
                if (createTime == null) createTime = orm.GetType().GetProperty("CreateTime");
                if (null != createTime)
                {
                    createTime.SetValue(orm, DateTime.Now);
                }

                var dateTime = orm.GetType().GetProperty("DateTime");
                var dateTimeVal = msgStruct.GetType().GetProperty("DateTime").GetValue(msgStruct);
                dateTime.SetValue(orm, dateTimeVal);
            }
            return orm;
        }

        /// <summary>
        /// Tbl轉報文格式
        /// </summary>
        public static T FromDBModel<T>(object msgStruct)
        {
            //產生ORM實例:
            T orm = Activator.CreateInstance<T>();

            var msgKV = msgStruct.GetType().GetProperties().ToDictionary(x => x.Name, x => x.GetValue(msgStruct));
            var ormFields = orm.GetType().GetFields();

            foreach (var field in ormFields)
            {
                if (msgKV.ContainsKey(field.Name))
                {
                    var value = msgKV[field.Name];

                    if (value.GetType() == typeof(char[]) && field.FieldType == typeof(string))
                    {
                        field.SetValue(orm, new string(value as char[]));
                    }

                    else if (value.GetType() == typeof(byte[]))
                    {

                        var byteData = value as byte[];
                        field.SetValue(orm, byteData.ToStr());
                    }

                    else if (value.GetType() == typeof(string))
                    {
                        var str = value as string;
                        var sizeConst = str.Count();

                        foreach (var NA1 in field.CustomAttributes.ElementAt(0).NamedArguments)
                        {
                            if (NA1.MemberName == "SizeConst")
                            {
                                sizeConst = Convert.ToInt32(NA1.TypedValue.Value);
                                break;
                            }
                        }
                        field.SetValue(orm, str.ToCByteArray(sizeConst));
                    }

                    else
                    {
                        field.SetValue(orm, value);
                    }
                }
            }

            return orm;
        }


        // 測試用 - 反射跟Manual Map時間差異性多少
        public static L1L2_302 Convert302DBModel(L1L2Rcv.Msg_302_CoilWeld msg)
        {

            var dbModel = new L1L2_302
            {
                MessageId = msg.MessageId,
                MessageLength = msg.MessageLength,
                DateTime = msg.DateTime,
                CoilID = msg.CoilNoID,
                WeldVoltageSettingFrontTorch = msg.WeldVoltageSettingFrontTorch,
                WeldVoltageSettingRearTorch = msg.WeldVoltageSettingRearTorch,
                WeldWireSpeedFrontTorch = msg.WeldWireSpeedFrontTorch,
                WeldWireSpeedRearTorch = msg.WeldWireSpeedRearTorch,
                WeldCurrent = msg.WeldCurrent,
                TorchCarriageWeldSpeed = msg.TorchCarriageWeldSpeed,
                OperatorSideFrontWeldGap = msg.OperatorSideFrontWeldGap,
                DriveSideRearWeldGap = msg.DriveSideRearWeldGap,
                StartPuddleTime = msg.StartPuddleTime,
                StopPuddleTime = msg.StopPuddleTime,
                WeldScheduleNumber = msg.WeldScheduleNumber,
                AnnealerPowerPercentage = msg.AnnealerPowerPercentage,
                WeldTempActPoint = msg.WeldTempActPoint,
                HeadEndLeaderWeldPointPosition = msg.HeadEndLeaderWeldPointPosition,
                HeadEndLeaderWeldPointDistanceFromPunchHole = msg.HeadEndLeaderWeldPointDistanceFromPunchHole,
                TailEndLeaderWeldPointPosition = msg.TailEndLeaderWeldPointPosition,
                TailEndLeaderWeldPointDistanceFromPunchHole = msg.TailEndLeaderWeldPointDistanceFromPunchHole
            };

            return dbModel;
        }
    }
}
