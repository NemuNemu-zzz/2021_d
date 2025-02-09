﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace kc3.d.tz.evolve {
    public class EvolveManager : MonoBehaviour {
        EvolveValue evolveValue;
        static int evolveIndex = 3; //ゲーム中に変更しセーブ。今はテスト用の仮。
        Transform testWoolTransform;
        readonly Color TRANSARENT = new Color(1, 1, 1, 0);
        readonly Color NORMAL_COLOR = new Color(1, 1, 1, 1);
        readonly Vector3 SMALL = new Vector3(0, 0, 0);
        readonly Vector3 NORMAL = new Vector3(1, 1, 1);
        [SerializeField] SpriteRenderer testWool;
        [SerializeField] Sprite[] wools;
        Dictionary<int, int> PointToMoveIndexConverter = new Dictionary<int, int>() {
            {8,2},
            {5,1},
            {2,-1},
            {0,-2}
        };
        void Start() {
            evolveValue = EvolveValue.instance;
            testWoolTransform = testWool.gameObject.GetComponent<Transform>();
            Evolve();
        }

        public void Evolve() {
            int num = (int)evolveValue.GetEvolveNum();
            int moveNum = 0;
            foreach(KeyValuePair<int,int> item in PointToMoveIndexConverter) {
                if (num > item.Key) {
                    moveNum = item.Value;
                    break;
                } else {
                    moveNum = 1;
                }
            }
            evolveIndex += moveNum;
            if (moveNum < 0) {
                moveNum = 0;
            } else if (moveNum > wools.Length - 1) {
                moveNum = wools.Length - 1;
            }
            Sequence sequence = DOTween.Sequence();
            sequence.Append(testWoolTransform.DOShakeScale(2, 0.6f,10,120,false))
                           .Join(testWool.DOColor(TRANSARENT, 2))
                           .Append(testWoolTransform.DOScale(SMALL, 0.1f))
                           .AppendCallback(() => {
                               testWool.sprite = wools[evolveIndex];
                           })
                           .Append(testWoolTransform.DOScale(NORMAL, 0.5f))
                           .Join(testWool.DOColor(NORMAL_COLOR, 0.5f));
        }
    }
}