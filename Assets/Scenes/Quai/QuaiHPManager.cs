
public class QuaiHPManager {

    private int HP;
    private int HPCurrent;

    public QuaiHPManager(int inputHP)
    {
        HP = HPCurrent = inputHP;
    }

    public void Damage(int dameageAmount)
    {
        if (HPCurrent < dameageAmount)
            HPCurrent = 0;
        else
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

    public int getHPMax()
    {
        return HP;
    }

    public void returnHP()
    {
        HPCurrent = HP;
    }
}
