
using Microsoft.Xna.Framework;
using MonoGame_Core.Scripts;

public class WeaponRepr: WorldObject {
    public WeaponRepr(string name,string texID,Vector2 pos)
    : base(name,texID,new string[] {"weapon","repr"},pos,3) {
        AddComponent(new WeaponData(this,"WeaponData"));
        CoroutineManager.Add(Coroutines.Reload((WeaponData)GetComponent("WeaponData")),"Reload",1,true);
    }
}