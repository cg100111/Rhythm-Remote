using UnityEngine;
using System.Collections;

interface IKey  {

    //設定Key的資訊
    void SetKeyInformation(float approchRateTime, int targetTime, ObjectPool poolObject);

    //回收
    void Collection();
}
