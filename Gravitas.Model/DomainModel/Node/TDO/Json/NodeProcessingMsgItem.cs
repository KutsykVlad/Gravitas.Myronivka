namespace Gravitas.Model.DomainModel.Node.TDO.Json
{
    public class NodeProcessingMsgItem
    {
        public ProcessingMsgType TypeId { get; set; }
        public string Text { get; set; }

        public NodeProcessingMsgItem(ProcessingMsgType typeId, string text)
        {
            TypeId = typeId;
            Text = text;
        }
    }
}