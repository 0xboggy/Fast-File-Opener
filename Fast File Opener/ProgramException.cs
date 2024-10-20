using System;

public class ProgramException : Exception
{
    private string error;
    private string title;
    public ProgramException(string error)
    {
        setError(error);
    }

    public ProgramException(string error,string title)
    {
        setError(error);
        setTitle(title);
    }

    private void setError(string error)
    {
        this.error = error;
    }

    public string getError()
    {
        return error;
    }

    private void setTitle(string title)
    {
        this.title = title;
    }

    public string getTitle()
    {
        return title;
    }
}