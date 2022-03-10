﻿namespace ConsoleMenu {
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

		public new MenuItem this[int id] {
			set {
				if (id != 0) {
					value.ID = id;
				}
				if (!Set(id, value)) Add(value);
			}
			get => this.FirstOrDefault(x => x.ID == id) ?? throw new IndexOutOfRangeException();
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
				int width = this.Max(x => x.Text.Length);
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
