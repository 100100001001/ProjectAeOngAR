﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아이템 타입 열거형(모음)
public enum ItemType
{
    MILK,
    FOOD
}

// Seializable : 사용자가 정의한 클래스 또는 구조체의 정보가 인스펙터에 노출되지 않음
//               하지만, System에서 제공하는 'Serializable' 속성을 한정하면 인스펙터에 노출시킬 수 있음
[System.Serializable]
public class Item
{
    public ItemType itemType; // 아이템 타입
    public string itemName;   // 아이템 이름
    public Sprite itemImage;  // 아이템 이미지

    // 아이템 사용 메서드 : 사용 성공 여부를 반환
    public bool User()
    {
        return false;
    }
}
