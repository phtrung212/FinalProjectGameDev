
using System.Diagnostics;

public class ExperenceManager
{
    QuaiHPManager mana;
    QuaiHPManager health;
    static int[] arrayHealth;
    static ulong[] arrayLv;
    static int[] arrayMana;
    private int lvCurent;
    private ulong ExperenceCurrent;

 
    public ExperenceManager(int level, ulong ExperenceCurrent, ref QuaiHPManager health, ref QuaiHPManager mana)
    {
        arrayLv = new ulong[100];
        arrayLv[0] = 100;
        arrayHealth = new int[100];
        arrayHealth[0] = 500;
        arrayMana = new int[100];
        arrayMana[0] = 100;
        for (int i = 1; i < arrayLv.Length; i++)
        {
            arrayLv[i] = arrayLv[i - 1] * 2;
            arrayHealth[i] = (int)(arrayHealth[i - 1] * 1.1);
            arrayMana[i] = (int)(arrayMana[i - 1] * 1.1);
        }
        this.mana = mana;
        this.health = health;
        this.ExperenceCurrent = ExperenceCurrent;
        lvCurent = level;
    }

    public void increase(int experence)
    {
        Debug.WriteLine("-----------");
        Debug.WriteLine(experence);
        ExperenceCurrent = ExperenceCurrent + (ulong)experence;
        while(ExperenceCurrent >= arrayLv[lvCurent])
        {
            health.setHPMax(lvCurent);
            ExperenceCurrent = ExperenceCurrent - arrayLv[lvCurent];
            lvCurent++;
            health.setHPMax(arrayHealth[lvCurent]);
            mana.setHPMax(arrayMana[lvCurent]);
            Debug.WriteLine(lvCurent);
        }
    }

    public float getExperencePercent()
    {
        return (float)ExperenceCurrent / arrayLv[lvCurent];
    }

    public ulong getExperenceNextLV()
    {
        return arrayLv[lvCurent];
    }
    public ulong getExperence()
    {
        return ExperenceCurrent;
    }
    
    public int getLevel()
    {
        return lvCurent + 1;
    }

    public static int getHealthMax(int lv)
    {
        return arrayHealth[lv];
    }

    public static int getManaMax(int lv)
    {
        return arrayMana[lv];
    }
}
