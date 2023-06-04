public class PipeTypeView : CustomPopupWindow
{
	public CustomEnumField<PipeEdgeType> PipeTypeField;

	public PipeTypeView(string labelText) : base (labelText, true)
	{
		PipeTypeField = new CustomEnumField<PipeEdgeType>("", PipeEdgeType.NO_PIPE);
		PipeTypeField.style
			.SetFontSize(9)
			.SetWidthFill();

		this.Add(PipeTypeField);
	}
}