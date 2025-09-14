using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 힙에서 사용할 아이템은 이 인터페이스를 구현해야 함
public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex { get; set; }
}
