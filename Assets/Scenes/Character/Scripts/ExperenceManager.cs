
using System.Diagnostics;

public class ExperenceManager
{
    int[] arrayLv;
    private int lvCurent;
    private int ExperenceCurrent;

    public ExperenceManager(int level, int ExperenceCurrent)
    {
        arrayLv = new int[100];
        arrayLv[0] = 100;
        for(int i = 1; i < arrayLv.Length; i++)
        {
            arrayLv[i] = arrayLv[i-1]*2;
        }
        this.ExperenceCurrent = ExperenceCurrent;
        lvCurent = level;
    }

    public void increase(int experence)
    {
        ExperenceCurrent = ExperenceCurrent + experence;
        while(ExperenceCurrent >= arrayLv[lvCurent])
        {
            ExperenceCurrent = ExperenceCurrent - arrayLv[lvCurent];
            lvCurent++;
            Debug.WriteLine(lvCurent);
        }
    }

    public float getExperencePercent()
    {
        return (float)ExperenceCurrent / arrayLv[lvCurent];
    }

    public int getExperenceNextLV()
    {
        return arrayLv[lvCurent];
    }
    public int getExperence()
    {
        return ExperenceCurrent;
    }
    
    public int getLevel()
    {
        return lvCurent + 1;
    }
}
