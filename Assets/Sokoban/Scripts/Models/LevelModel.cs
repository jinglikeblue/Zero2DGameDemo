using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace Sokoban
{
    public class LevelModel
    {
        /**
        * 关卡ID 
        */
        public int id;

        /// <summary>
        /// 阻挡
        /// </summary>
        public AUnitVO[] blocks;

        /// <summary>
        /// 角色初始位置
        /// </summary>
        public AUnitVO[] roles;

        /// <summary>
        /// 箱子的初始位置
        /// </summary>
        public AUnitVO[] boxes;

        /// <summary>
        /// 目标点
        /// </summary>
        public AUnitVO[] targets;

        private XmlDocument _xml;

        Dictionary<string, AUnitVO> _unitDic = new Dictionary<string, AUnitVO>();

        public LevelModel(int levelId)
        {
            id = levelId;
            TextAsset data = Resources.Load<TextAsset>("Levels/level" + levelId);
            var xml = new XmlDocument();
            xml.LoadXml(data.text);
            _xml = xml;

            blocks = GetUnitVO("blocks", EUnitType.BLOCK);
            roles = GetUnitVO("roles", EUnitType.ROLE);
            boxes = GetUnitVO("boxs", EUnitType.BOX);
            targets = GetUnitVO("targets", EUnitType.TARGET);
        }

        AUnitVO[] GetUnitVO(string type, EUnitType unitType)
        {
            var levelNode = _xml.SelectSingleNode("level");
            XmlNodeList unitNodes = levelNode.SelectSingleNode(type).SelectNodes("unit");
            AUnitVO[] units = new AUnitVO[unitNodes.Count];
            for (int i = 0; i < units.Length; i++)
            {
                var attr = unitNodes[i] as XmlElement;
                var x = attr.GetAttribute("x");
                var y = attr.GetAttribute("y");

                AUnitVO unitVO = new AUnitVO();
                unitVO.x = ushort.Parse(x);
                unitVO.y = ushort.Parse(y);
                unitVO.type = unitType;
                units[i] = unitVO;
                _unitDic[GetPosFlag(unitVO.x, unitVO.y)] = unitVO;
            }
            return units;
        }

        AUnitVO GetUnitVO(ushort x, ushort y)
        {
            string flag = GetPosFlag(x, y);
            if(_unitDic.ContainsKey(flag))
            {
                return _unitDic[flag];
            }
            return null;
        }

        string GetPosFlag(ushort x, ushort y)
        {
            return string.Format("{0}_{1}", x, y);
        }

        public bool IsBlock(ushort x, ushort y)
        {
            return IsType(x, y, EUnitType.BLOCK);       
        }

        public bool IsTarget(ushort x, ushort y)
        {
            return IsType(x, y, EUnitType.TARGET);
        }

        public bool IsBox(ushort x, ushort y)
        {
            return IsType(x, y, EUnitType.BOX);
        }

        

        public bool IsType(ushort x, ushort y, EUnitType unitType)
        {
            var vo = GetUnitVO(x, y);
            if (null == vo || vo.type != unitType)
            {
                return false;
            }

            return true;
        }
    }
}
