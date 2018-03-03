using Sl;

public class PlayerWrapper : DSValueWrapper<Player, Data>
{
    public DSValueWrapper<string, Data> Id { get { return new DSValueWrapper<string, Data>(data_, "id_", this); } }
    public DSValueWrapper<string, Data> Name { get { return new DSValueWrapper<string, Data>(data_, "name_", this); } }

    public PlayerWrapper(Data data, string name, IDSValueWrapper parent)
        : base(data, name, parent) { }
}
