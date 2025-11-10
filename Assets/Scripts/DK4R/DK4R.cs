using System.Collections.Generic;


namespace DK4R
{
	public class Cell
	{
		public int CellId;
	}

	public class TileCell : Cell
	{
	}

	public class HexagonCell : Cell
	{
	}

	public class Map
	{
		private List<Cell> m_Cells;
		private List<Event> m_Events;
	}

	public class VillageMap : Map
	{
	}

	public class BattleMap : Map
	{
	}


	public class Pawn
	{

	}

	public class VillagePawn : Pawn
	{
	}

	public class BattlePawn : Pawn
	{
	}

	public class Event
	{
		//private List<>
	}


	public class UISpawnPopup { }
	public class UIStatusPopup { }
	public class UIBattleAnimationPopup { }
	public class UIMapPanel { }
	public class UIMapPopup { }
	public class UIInventoryPopup { }
	public class UISettingsPopup { }

	public class UIScenePanel { }
	public class UIHScenePanel { }	
}