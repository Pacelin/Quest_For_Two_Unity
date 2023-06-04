using UnityEngine;
using UnityEngine.UIElements;

public class PipeSettingsView : CustomPopupWindow
{
	public CustomObjectField<Sprite> SpriteField;
	public CustomButton AddPipeButton;
	public CustomButton GeneratePuzzleButton;

	public PipeTypeView TopPipeTypeView;
	public PipeTypeView RightPipeTypeView;
	public PipeTypeView BottomPipeTypeView;
	public PipeTypeView LeftPipeTypeView;

	public PipeView PipeView;

	private CustomPopupWindow _container;
	private VisualElement _pipeEdges;

	private static readonly Length PERCENT_33 = Length.Percent(100f / 3);
	private static readonly Length PERCENT_66 = Length.Percent(200f / 3);

	public PipeSettingsView() : base("Pipe Settings")
	{
		_container = new CustomPopupWindow("Pipe Edges", true);
		_container.style.SetMarginBottom(10);
		this.Add(_container);

		AddPipeEdges();

		SpriteField = new CustomObjectField<Sprite>("Pipe Sprite");
		SpriteField.style
			.SetWidthFill()
			.SetMarginBottom(10);
		this.Add(SpriteField);

		AddPipeButton = new CustomButton("Add pipe");
		AddPipeButton.style
			.SetWidthFill()
			.SetMarginBottom(10);
		AddPipeButton.Disable();
		this.Add(AddPipeButton);

		GeneratePuzzleButton = new CustomButton("Create Puzzle");
		GeneratePuzzleButton.style
			.SetWidthFill()
			.SetGrow();
		GeneratePuzzleButton.Disable();
		this.Add(GeneratePuzzleButton);

		SpriteField.OnValueChanged += newValue =>
		{
			if (newValue == null) AddPipeButton.Disable();
			else AddPipeButton.Enable();
		};
	}

	public void ClearSettings()
	{
		AddPipeButton.Disable();
		SpriteField.SetValue(null);
		TopPipeTypeView.PipeTypeField.SetValue(PipeEdgeType.NO_PIPE);
		RightPipeTypeView.PipeTypeField.SetValue(PipeEdgeType.NO_PIPE);
		BottomPipeTypeView.PipeTypeField.SetValue(PipeEdgeType.NO_PIPE);
		LeftPipeTypeView.PipeTypeField.SetValue(PipeEdgeType.NO_PIPE);
	}

	private void AddPipeEdges()
	{
		_pipeEdges = new VisualElement();
		_pipeEdges.style.SetSize(360, 360);

		_container.Add(_pipeEdges);
		TopPipeTypeView = new PipeTypeView("TOP");
		TopPipeTypeView.style
			.SetSize(PERCENT_33, PERCENT_33)
			.SetPositionAbsolute()
			.SetTop(0)
			.SetLeft(PERCENT_33);

		RightPipeTypeView = new PipeTypeView("RIGHT");
		RightPipeTypeView.style
			.SetSize(PERCENT_33, PERCENT_33)
			.SetPositionAbsolute()
			.SetTop(PERCENT_33)
			.SetLeft(PERCENT_66);

		BottomPipeTypeView = new PipeTypeView("BOTTOM");
		BottomPipeTypeView.style
			.SetSize(PERCENT_33, PERCENT_33)
			.SetPositionAbsolute()
			.SetTop(PERCENT_66)
			.SetLeft(PERCENT_33);

		LeftPipeTypeView = new PipeTypeView("LEFT");
		LeftPipeTypeView.style
			.SetSize(PERCENT_33, PERCENT_33)
			.SetPositionAbsolute()
			.SetTop(PERCENT_33)
			.SetLeft(0);

		PipeView = new PipeView();
		PipeView.style
			.SetSize(Length.Percent(30), Length.Percent(30))
			.SetPositionAbsolute()
			.SetTop(Length.Percent(35))
			.SetLeft(Length.Percent(35));

		_pipeEdges.Add(TopPipeTypeView);
		_pipeEdges.Add(RightPipeTypeView);
		_pipeEdges.Add(BottomPipeTypeView);
		_pipeEdges.Add(LeftPipeTypeView);
		_pipeEdges.Add(PipeView);

		TopPipeTypeView.PipeTypeField.OnValueChanged += PipeView.Pipe.TopEdge.SetType;
		RightPipeTypeView.PipeTypeField.OnValueChanged += PipeView.Pipe.RightEdge.SetType;
		BottomPipeTypeView.PipeTypeField.OnValueChanged += PipeView.Pipe.BottomEdge.SetType;
		LeftPipeTypeView.PipeTypeField.OnValueChanged += PipeView.Pipe.LeftEdge.SetType;
	}
}