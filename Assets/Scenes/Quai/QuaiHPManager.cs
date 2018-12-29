
public class QuaiHPManager {

    private int HP;
    private int HPCurrent;

    public QuaiHPManager(int inputHP)
    {
        HP = HPCurrent = inputHP;
    }

    public void Damage(int dameageAmount)
    {
        HPCurrent = HPCurrent - dameageAmount;
    }

    public float getHealthPercent()
    {
        return (float)HPCurrent / HP;
    }
    public int getHP()
    {
        return HPCurrent;
    }

    public void returnHP()
    {
        HPCurrent = HP;
    }
}
