
using Microsoft.Xna.Framework;
using MonoGame_Core.Scripts;

public class Weapon: InventoryItem {
    public Weapon(string name,string texID,Vector2 pos)
    : base(name,texID,pos,InventoryItemShapeData.Shapes.Square) {
        AddComponent(new WeaponData(this,"WeaponData"));
        CoroutineManager.Add(Coroutines.Reload((WeaponData)GetComponent("WeaponData")),"Reload",1,true);
        AddBehavior("shoot",ShipBehaviors.ShootOnClick);
    }
}