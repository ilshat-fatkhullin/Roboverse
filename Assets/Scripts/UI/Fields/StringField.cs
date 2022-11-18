namespace Assets.Scripts.UI.Fields
{
    public class StringField : RoboverseField<string>
    {
        protected override string ParseValue(string value)
        {
            return value;
        }
    }
}