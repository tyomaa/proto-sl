using Sl;

public class DataWrapper : DSValueWrapper<Data, Data>
{
    public PlayerWrapper Player { get { return new PlayerWrapper(data_, "player_", this); } }
    public DSValueWrapper<int, Data> Gold { get { return new DSValueWrapper<int, Data>(data_, "gold_", this); } }
    public MapValueWrapper<string, int, Data> Resources { get { return new MapValueWrapper<string, int, Data>(data_, "resources_", this); } }

    public DataWrapper(Data data, string name, IDSValueWrapper parent)
        : base(data, name, parent) { }
}
