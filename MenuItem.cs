namespace ConsoleMenu {
	public class MenuItem : IEquatable<MenuItem> {
		public int ID { get; set; }
		public string Text { get; set; }
		public bool IsEnabled { get; set; }

		public MenuItem() : this("") { }
		public MenuItem(string text) : this(text, true) { }
		public MenuItem(string text, bool enabled) : this(0, text, enabled) { }
		public MenuItem(int id, string text, bool enabled) {
			(ID, Text, IsEnabled) = (id, text, enabled);
		}

		public void Deconstruct(out int id, out string text, out bool enabled) {
			id = ID;
			text = Text;
			enabled = IsEnabled;
		}

		public bool Equals(MenuItem? other) {
			if (other is null) return false;
			if (ReferenceEquals(this, other)) return true;
			return ID == other.ID && Text == other.Text && IsEnabled == other.IsEnabled;
		}

		public override bool Equals(object? obj) {
			return Equals(obj as MenuItem);
		}
		public override int GetHashCode() {
			return HashCode.Combine(ID, Text, IsEnabled);
		}
		public static bool operator ==(MenuItem? left, MenuItem? right) {
			return EqualityComparer<MenuItem>.Default.Equals(left, right);
		}
		public static bool operator !=(MenuItem? left, MenuItem? right) {
			return !(left == right);
		}
		public static implicit operator MenuItem(string text) => new(text);
		public static implicit operator MenuItem((string, bool) value) => new CheckMenuItem(value.Item1) { IsChecked = value.Item2 };
	}
}
