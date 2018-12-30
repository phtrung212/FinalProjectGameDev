
public class ExperenceManager
{
    int[] arrayLv;
    private int lvCurent;
    private int ExperenceCurrent;

    public ExperenceManager(int level)
    {
        arrayLv = new int[100];
        arrayLv[0] = 100;
        for(int i = 1; i < arrayLv.Length; i++)
        {
            arrayLv[i] = arrayLv[i-1]*2;
        }
        ExperenceCurrent = 0;
        lvCurent = level;
    }

    public void increase(int experence)
    {
        ExperenceCurrent = ExperenceCurrent + experence;
        while(ExperenceCurrent >= arrayLv[lvCurent])
        {
            ExperenceCurrent = ExperenceCurrent - arrayLv[lvCurent];
            lvCurent++;
        }
    }

    public float getExperencePercent()
    {
        return (float)ExperenceCurrent / arrayLv[lvCurent];
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
