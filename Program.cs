namespace ConsoleMenu {
	public static class Program {
		public static void Main(string[] args) {
			Console.WriteLine("test test");
			Menu menu = new("Test Menu") {
				[1] = "Foo",
				[2] = ("Bar", false),
				[0] = "Baz",
				[0] = "Xyz",
				[3] = "Test"
			};
			int selected = menu.Show();
			bool? bar = (menu[2] as CheckMenuItem)?.IsChecked;
			Console.WriteLine($"Selected = [{selected}] {menu[selected].Text}");
			Console.WriteLine($"Bar = {(bar == true ? "true" : bar == false ? "false" : "null")}");
		}
	}
}
