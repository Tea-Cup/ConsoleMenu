using System.Text;

namespace ConsoleMenu {
	public class Menu : List<MenuItem> {
		private static (ConsoleColor back, ConsoleColor fore) CurrentColor {
			get => (Console.BackgroundColor, Console.ForegroundColor);
			set => (Console.BackgroundColor, Console.ForegroundColor) = value;
		}
		private static (int left, int top) Position {
			set => (Console.CursorLeft, Console.CursorTop) = value;
		}

		public string Title { get; set; } = "";
		public ConsoleColor BackColor { get; set; }
		public ConsoleColor ForeColor { get; set; }
		private (ConsoleColor back, ConsoleColor fore) MainColor => (BackColor, ForeColor);
		private (ConsoleColor back, ConsoleColor fore) RevsColor => (ForeColor, BackColor);

		public new string this[int id] {
			set {
				if (id == 0) Add(value, false);
				else if (!Set(id, value)) Add(id, value);
			}
			get => this.FirstOrDefault(x=>x.ID == id)?.Text ?? throw new IndexOutOfRangeException();
		}

		public Menu() {
			(BackColor, ForeColor) = CurrentColor;
		}
		public Menu(string title) : this() {
			Title = title;
		}

		public int Add(string text) {
			return Add(0, text, true);
		}
		public int Add(string text, bool enabled) {
			return Add(0, text, enabled);
		}
		public int Add(int id, string text) {
			return Add(id, text, true);
		}
		public int Add(int id, string text, bool enabled) {
			MenuItem item = new(id, text, enabled);
			Add(item);
			return item.ID;
		}
		public bool Set(int id, string text, bool? enabled = null) {
			if (id == 0) return false;
			foreach(MenuItem item in this) {
				if(item.ID == id) {
					item.Text = text;
					if(enabled.HasValue) item.IsEnabled = enabled.Value;
					return true;
				}
			}
			return false;
		}

		public int Show(bool clear = true) {
			if (Count == 0) return 0;
			if (Console.CursorLeft > 0) Console.WriteLine();
			int top = Console.CursorTop;
			int sel = 0;
			var old = (CurrentColor, Console.CursorVisible);
			Console.CursorVisible = false;

			while (true) {
				Position = (0, top);
				CurrentColor = MainColor;
				if (!string.IsNullOrEmpty(Title))
					Console.WriteLine(Title);

				bool hasEnabled = false;
				int width = this.Max(x => x.Text.Length);
				for (int i = 0; i < Count; i++) {
					CurrentColor = i == sel ? RevsColor : MainColor;
					if (base[i].IsEnabled) {
						hasEnabled = true;
						Console.Write("  ");
					}
					Console.Write(base[i].Text.PadRight(width));
					Console.WriteLine("  ");
				}
				if (!hasEnabled) return 0;

				ConsoleKey key = Console.ReadKey(true).Key;
				if(key == ConsoleKey.UpArrow) {
					do {
						if (--sel < 0) sel = Count - 1;
					} while (!base[sel].IsEnabled);
				} else
				if (key == ConsoleKey.DownArrow) {
					do {
						if (++sel >= Count) sel = 0;
					} while (!base[sel].IsEnabled);
				}
				if (key == ConsoleKey.Enter) {
					break;
				}
			}

			(CurrentColor, Console.CursorVisible) = old;
			return base[sel].ID;
		}
	}
}
