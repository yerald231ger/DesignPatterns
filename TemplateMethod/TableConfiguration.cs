using TemplateMethod.Pattern;

namespace TemplateMethod;

public class TableConfiguration
{
    private Material _material;
    private int _numberOfChairs = 4;
    private int _numberOfBenches = 1;
    private float _areaTableCm = 760f;
    
    public Material GetMaterial() => _material;
    public int GetNumberOfChairs() => _numberOfChairs;
    public int GetNumberOfBenches() => _numberOfBenches;
    public float GetAreaTable() => _areaTableCm;
    public void SetMaterial(Material material) => _material = material;
    
    public DefaultTemplateMethod GetTemplate() =>
        _material switch 
        {
            Material.Walnut => new WalnutQuote(Clone()),
            Material.Pine => new PineQuote(Clone()),
            Material.Cherry => new CherryDynamicQuote(Clone()),
            _ => new BasicQuote(Clone())
        };

    public void SetTableArea(int tableArea)
    {
        _areaTableCm = tableArea;
    }

    private TableConfiguration Clone()
    {
        return new TableConfiguration
        {
            _material = _material,
            _numberOfChairs = _numberOfChairs,
            _numberOfBenches = _numberOfBenches,
            _areaTableCm = _areaTableCm
        };
    }
}

public enum Material
{
    Pine,
    Oak,
    Mahogany,
    Walnut,
    Cherry
}