namespace ConsoleMenu {
	public static class Program {
		public static void Main(string[] args) {
			new Menu("Test Menu") {
				[1] = "Foo",
				[2] = "Bar",
				[0] = "Baz",
				[0] = "Xyz",
				[3] = "Test"
			}.Show();
		}
	}
}
