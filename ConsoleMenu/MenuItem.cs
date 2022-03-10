namespace ConsoleMenu {
	/// <summary>Container of a single item in <see cref="Menu"/></summary>
	public class MenuItem : IEquatable<MenuItem> {
		/// <summary>
		/// <para>Numeric identifier of item.</para>
		/// <para>Not an index. Uniqueness not guaranteed.</para>
		/// </summary>
		public int ID { get; set; }
		/// <summary>Text displayed on item</summary>
		public string Text { get; set; }
		/// <summary>If <see langword="false" />, this item will not be selectable</summary>
		public bool IsEnabled { get; set; }
		/// <summary>
		/// <para>Checkbox value of item.</para>
		/// <para>If <see langword="null" />, thne this item does not have a checkbox</para>
		/// </summary>
		public bool? IsChecked { get; set; }

		/// <summary>Constructs a new empty instance of <see cref="MenuItem"/></summary>
		public MenuItem() : this("") { }
		/// <summary>Constructs a new instance of <see cref="MenuItem"/></summary>
		/// <param name="text">Text displayed on item</param>
		public MenuItem(string text) : this(text, true) { }
		/// <summary>Constructs a new instance of <see cref="MenuItem"/></summary>
		/// <param name="text">Text displayed on item</param>
		/// <param name="enabled">If <see langword="false" />, this item will not be selectable</param>
		public MenuItem(string text, bool enabled) : this(0, text, enabled) { }
		/// <summary>Constructs a new instance of <see cref="MenuItem"/></summary>
		/// <param name="id">
		/// <para>Numeric identifier of item.</para>
		/// <para>Not an index. Uniqueness not guaranteed.</para>
		/// </param>
		/// <param name="text">Text displayed on item</param>
		/// <param name="enabled">If <see langword="false" />, this item will not be selectable</param>
		public MenuItem(int id, string text, bool enabled) {
			(ID, Text, IsEnabled) = (id, text, enabled);
		}

		/// <summary></summary>
		/// <param name="id">
		/// <para>Numeric identifier of item.</para>
		/// <para>Not an index. Uniqueness not guaranteed.</para>
		/// </param>
		/// <param name="text">Text displayed on item</param>
		/// <param name="enabled">If <see langword="false" />, this item will not be selectable</param>
		/// <param name="check">
		/// <para>Checbox value of item.</para>
		/// <para>If <see langword="null" />, thne this item does not have a checkbox</para>
		/// </param>
		public void Deconstruct(out int id, out string text, out bool enabled, out bool? check) {
			id = ID;
			text = Text;
			enabled = IsEnabled;
			check = IsChecked;
		}

		/// <summary>Determines whether the specified object is equal to the current object</summary>
		/// <param name="other">The object to compare with the current object</param>
		/// <returns><see langword="true" /> if the specified object is equal to the current object; otherwise, <see langword="false" /></returns>
		public bool Equals(MenuItem? other) {
			if (other is null) return false;
			if (ReferenceEquals(this, other)) return true;
			return ID == other.ID && Text == other.Text && IsEnabled == other.IsEnabled && IsChecked == other.IsChecked;
		}

		/// <summary>Determines whether the specified object is equal to the current object</summary>
		/// <param name="obj">The object to compare with the current object</param>
		/// <returns><see langword="true" /> if the specified object is equal to the current object; otherwise, <see langword="false" /></returns>
		public override bool Equals(object? obj) {
			return Equals(obj as MenuItem);
		}
		/// <summary>Calculates the hash code of this object</summary>
		/// <returns>The calculated hash code</returns>
		public override int GetHashCode() {
			return HashCode.Combine(ID, Text, IsEnabled, IsChecked);
		}
		/// <summary>Determines whether the specified objects are equal</summary>
		/// <param name="left">First object</param>
		/// <param name="right">Second object</param>
		/// <returns><see langword="true" /> if objects are equal; otherwise, <see langword="false" /></returns>
		public static bool operator ==(MenuItem? left, MenuItem? right) {
			return EqualityComparer<MenuItem>.Default.Equals(left, right);
		}
		/// <summary>Determines whether the specified objects are not equal</summary>
		/// <param name="left">First object</param>
		/// <param name="right">Second object</param>
		/// <returns><see langword="true" /> if objects are not equal; otherwise, <see langword="false" /></returns>
		public static bool operator !=(MenuItem? left, MenuItem? right) {
			return !(left == right);
		}

		/// <summary>Constructs a new <see cref="MenuItem"/> from a <see cref="string"/></summary>
		/// <param name="text">Text displayed on item</param>
		public static implicit operator MenuItem(string text) => new(text);
		/// <summary>Constructs a new <see cref="MenuItem"/> from a pair of <see cref="string"/> and <see cref="bool"/></summary>
		/// <param name="value">Pair of <u>text displayed on item</u> and <u>checkbox value of item</u></param>
		public static implicit operator MenuItem((string, bool) value) => new(value.Item1) { IsChecked = value.Item2 };
	}
}
