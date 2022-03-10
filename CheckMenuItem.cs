namespace ConsoleMenu {
	public class CheckMenuItem : MenuItem, IEquatable<CheckMenuItem> {
		public bool IsChecked { get; set; }

		public CheckMenuItem() : base() { }
		public CheckMenuItem(string text) : base(text) { }
		public CheckMenuItem(string text, bool enabled) : base(text, enabled) { }
		public CheckMenuItem(int id, string text, bool enabled) : base(id, text, enabled) { }

		public void Deconstruct(out int id, out string text, out bool enabled, out bool check) {
			id = ID;
			text = Text;
			enabled = IsEnabled;
			check = IsChecked;
		}

		public bool Equals(CheckMenuItem? other) {
			return base.Equals(other) && other.IsChecked == IsChecked;
		}

		public override bool Equals(object? obj) {
			return Equals(obj as CheckMenuItem);
		}
		public override int GetHashCode() {
			return HashCode.Combine(ID, Text, IsEnabled, IsChecked);
		}
		public static bool operator ==(CheckMenuItem? left, CheckMenuItem? right) {
			return EqualityComparer<CheckMenuItem>.Default.Equals(left, right);
		}
		public static bool operator !=(CheckMenuItem? left, CheckMenuItem? right) {
			return !(left == right);
		}
		public static implicit operator CheckMenuItem((string, bool) value) => new(value.Item1) { IsChecked = value.Item2 };
	}
}
