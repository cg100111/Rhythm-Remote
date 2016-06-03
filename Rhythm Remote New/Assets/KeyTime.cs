using System;

public class KeyTime {
    
    private int _pos;
    private int _time;

    public KeyTime(int pos, int time)
    {
        _pos = pos;
        _time = time;
    }

    public int GetPos()
    {
        return _pos;
    }

    public int GetTime()
    {
        return _time;
    }
}
