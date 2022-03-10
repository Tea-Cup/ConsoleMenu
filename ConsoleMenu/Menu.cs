namespace ConsoleMenu {
	/// <summary>Menu container of <see cref="MenuItem"/>s</summary>
	public class Menu : List<MenuItem> {
		private static (ConsoleColor back, ConsoleColor fore) CurrentColor {
			get => (Console.BackgroundColor, Console.ForegroundColor);
			set => (Console.BackgroundColor, Console.ForegroundColor) = value;
		}
		private static (int left, int top) Position {
			set => (Console.CursorLeft, Console.CursorTop) = value;
		}

		/// <summary>Optional text displayed at the top of menu</summary>
		public string Title { get; set; } = "";
		/// <summary>Background color of menu</summary>
		public ConsoleColor BackColor { get; set; }
		/// <summary>Foreground color of menu</summary>
		public ConsoleColor ForeColor { get; set; }
		private (ConsoleColor back, ConsoleColor fore) MainColor => (BackColor, ForeColor);
		private (ConsoleColor back, ConsoleColor fore) RevsColor => (ForeColor, BackColor);

		/// <summary>Indexer primarily made for object member initialization</summary>
		/// <param name="id">ID of menu item. If <c>0</c>, then item is disabled</param>
		/// <returns>First <see cref="MenuItem"/> with specified id</returns>
		/// <exception cref="IndexOutOfRangeException">Thrown if no items found with specified id</exception>
		public new MenuItem this[int id] {
			set {
				if (id != 0) {
					value.ID = id;
				} else {
					value.IsEnabled = false;
				}
				if (!Set(id, value)) Add(value);
			}
			get => this.FirstOrDefault(x => x.ID == id) ?? throw new IndexOutOfRangeException();
		}

		/// <summary>Creates a new instance of <see cref="Menu"/></summary>
		public Menu() {
			(BackColor, ForeColor) = CurrentColor;
		}
		/// <summary>Creates a new instance of <see cref="Menu"/> and changes its Title</summary>
		/// <param name="title">Value for <see cref="Title"/> property</param>
		public Menu(string title) : this() {
			Title = title;
		}

		/// <summary>Constructs and adds a new instance of <see cref="MenuItem"/> with specified text</summary>
		/// <param name="text">Text of a new <see cref="MenuItem"/></param>
		/// <returns><see cref="MenuItem.ID"/> of a new item</returns>
		public int Add(string text) {
			return Add(0, text, true);
		}
		/// <summary>Constructs and adds a new instance of <see cref="MenuItem"/> with specified text</summary>
		/// <param name="text">Text of a new <see cref="MenuItem"/></param>
		/// <param name="enabled">Whether or not this item is enabled</param>
		/// <returns><see cref="MenuItem.ID"/> of a new item</returns>
		public int Add(string text, bool enabled) {
			return Add(0, text, enabled);
		}
		/// <summary>Constructs and adds a new instance of <see cref="MenuItem"/> with specified text</summary>
		/// <param name="id"><see cref="MenuItem.ID"/> of a new item</param>
		/// <param name="text">Text of a new <see cref="MenuItem"/></param>
		/// <returns><see cref="MenuItem.ID"/> of a new item</returns>
		public int Add(int id, string text) {
			return Add(id, text, true);
		}
		/// <summary>Constructs and adds a new instance of <see cref="MenuItem"/> with specified text</summary>
		/// <param name="id"><see cref="MenuItem.ID"/> of a new item</param>
		/// <param name="text">Text of a new <see cref="MenuItem"/></param>
		/// <param name="enabled">Whether or not this item is enabled</param>
		/// <returns><see cref="MenuItem.ID"/> of a new item</returns>
		public int Add(int id, string text, bool enabled) {
			MenuItem item = new(id, text, enabled);
			Add(item);
			return item.ID;
		}

		/// <summary>Replaces a <see cref="MenuItem"/> with specified id with another one.</summary>
		/// <param name="id"><see cref="MenuItem.ID"/> of target item</param>
		/// <param name="item">Replacement item</param>
		/// <returns><see langword="true" /> if replacement occured.</returns>
		public bool Set(int id, MenuItem item) {
			if (id == 0) return false;
			for (int i = 0; i < Count; ++i) {
				if (base[i].ID == id) {
					base[i] = item;
					item.ID = id;
					return true;
				}
			}
			return false;
		}

		/// <summary>Displays this menu and enters a loop until item is chosen</summary>
		/// <param name="clear">If <see langword="true" />, then menu is erased from console afterwards</param>
		/// <returns><see cref="MenuItem.ID"/> of selected item</returns>
		public int Show(bool clear = true) {
			if (Count == 0) return 0;
			if (Console.CursorLeft > 0) Console.WriteLine();
			int top = Console.CursorTop;
			int sel = 0;
#pragma warning disable CA1416 // Validate platform compatibility
			var old = (CurrentColor, Console.CursorVisible);
#pragma warning restore CA1416 // Validate platform compatibility
			Console.CursorVisible = false;

			while (true) {
				Position = (0, top);
				CurrentColor = MainColor;
				if (!string.IsNullOrEmpty(Title))
					Console.WriteLine(Title);

				bool hasEnabled = false;
				int width = this.Max(x => x.Text.Length + (x.IsEnabled ? 0 : -4));
				for (int i = 0; i < Count; i++) {
					if (base[i].IsEnabled) hasEnabled = true;
					PrintMenuItem(base[i], width, i == sel);
				}
				if (!hasEnabled) return 0;

				ConsoleKey key = Console.ReadKey(true).Key;
				if (key == ConsoleKey.UpArrow) {
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
					if (base[sel].IsChecked is not null) base[sel].IsChecked = !base[sel].IsChecked;
					else break;
				}
			}

			(CurrentColor, Console.CursorVisible) = old;
			if (clear) {
				Position = (0, top);
				string line = new(' ', Console.BufferWidth);
				if (!string.IsNullOrEmpty(Title)) Console.WriteLine(line);
				for (int i = 0; i < Count; ++i) Console.WriteLine(line);
				Position = (0, top);
			}

			return base[sel].ID;
		}

		private void PrintMenuItem(MenuItem item, int width, bool sel) {
			if (item.IsChecked is not null) PrintCheckMenuItem(item, width, sel);
			else PrintStringMenuItem(item, width, sel);
		}
		private void PrintStringMenuItem(MenuItem item, int width, bool sel) {
			CurrentColor = sel ? RevsColor : MainColor;
			if (item.IsEnabled) Console.Write("    ");
			Console.Write(item.Text.PadRight(width));
			Console.WriteLine("  ");
		}
		private void PrintCheckMenuItem(MenuItem item, int width, bool sel) {
			CurrentColor = sel ? RevsColor : MainColor;

			Console.Write('[');
			if (item.IsChecked == true) Console.Write('X');
			else Console.Write(' ');
			Console.Write("] ");
			Console.Write(item.Text.PadRight(width));
			Console.WriteLine("  ");
		}
	}
}
