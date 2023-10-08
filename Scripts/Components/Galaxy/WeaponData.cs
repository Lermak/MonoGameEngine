using MonoGame_Core.Scripts;

public class WeaponData: Component {
    public float reloadSpeed;
    public bool CanFire;
    public WeaponData(GameObject go, string name)
    : base(go, name) {
        reloadSpeed = 200;
        CanFire = true;
    }

}