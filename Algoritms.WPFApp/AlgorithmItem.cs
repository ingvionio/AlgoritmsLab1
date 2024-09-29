public class AlgorithmItem
{
    public string Name { get; set; }
    public bool IsSelected { get; set; }

    public AlgorithmItem(string name, bool isSelected = false)
    {
        Name = name;
        IsSelected = isSelected;
    }
}