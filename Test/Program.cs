using ConsoleMenu;

Console.WriteLine("test test");
Menu menu = new("Test Menu") {
	[1] = "Foo",
	[2] = ("Bar", false),
	[0] = "(Baz)",
	[0] = "(Xyz)",
	[3] = "Test"
};
int selected = menu.Show();
bool bar = menu[2].IsChecked == true;
Console.WriteLine($"Selected = [{selected}] {menu[selected].Text}");
Console.WriteLine($"Bar = {(bar ? "true" : "false")}");