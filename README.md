# ConsoleMenu
Simple implementation of console menu without flickering and real-time user input.

Supports:
* Plain string
* Checkboxes
* Disabled items
* Colors

## Usage
API was made with simplicity and ease of use in mind.

Declare a menu without any extra classes:
```cs
Menu menu = new("Test Menu") {
	[1] = "Foo",
	[2] = ("Bar", false),
	[0] = "(Baz)",
	[0] = "(Xyz)",
	[3] = "Test"
};
```
Here, "first column" (in [square] brackets) set item ID's. If ID is `0`, then the item is disabled.  
(Static analyzers will complain about duplicate indices, but you can ignore them in case of `0`)  
Users can not select a disabled item.

Second column is menu item content. If it's just a string, then it will be displayed as a simple item.  
Make use of C# tuples to specify a pair of string and boolean to create an item with checkbox. Boolean value will be checkbox initial checked state.

Example above will create this kind of menu:  
![Menu in console](https://i.imgur.com/X5l0b61.png)

User then can use arrow buttons to navigate Up and Down:  
![Menu navigation](https://i.imgur.com/r77zA5J.gif)

When Enter key is pressed, menu is closing, returning last selected item ID.  
If Enter is pressed when checkbox item is selected, it's checked state is toggled.  
![Menu interaction](https://i.imgur.com/gU1EOVS.gif)

Last selected menu item ID is returned by `Show` method of a menu instance. It can be used to find `MenuItem` itself:
```cs
int selected = menu.Show();
Console.WriteLine($"Selected = [{selected}] {menu[selected].Text}");
```

Checkbox items can be accessed with their ID too:
```
bool bar = menu[2].IsChecked == true;
Console.WriteLine($"Bar = {(bar ? "true" : "false")}");
```

## Colors
Menu inherits current console color at the time of it's construction:
```cs
Console.BackgroundColor = ConsoleColor.White;
Console.ForegroundColor = ConsoleColor.Black;
Console.WriteLine("test test");
Menu menu = new("Test Menu") {
	[1] = "Foo",
	[2] = ("Bar", false),
	[0] = "(Baz)",
	[0] = "(Xyz)",
	[3] = "Test"
};
```
![Console with changed colors](https://i.imgur.com/9oOVxGk.png)

But you can specify colors yourself too:
```cs
Console.WriteLine("test test");
Menu menu = new("Test Menu") {
	BackColor = ConsoleColor.White,
	ForeColor = ConsoleColor.Black,
	[1] = "Foo",
	[2] = ("Bar", false),
	[0] = "(Baz)",
	[0] = "(Xyz)",
	[3] = "Test"
};
```
![Console with changed colors](https://i.imgur.com/gn5gIJ7.png)

Selected item will have its Background and Foreground colors inverted:
![Selection has colors inverted](https://i.imgur.com/vdxHOXP.gif)

## Documentation
Full documentation included in source code using standard XML-docs.  
There really isn't much going on. You will only need a `ConsoleMenu.Menu` class.

# Installation
Download a `.nupkg` file from [Releases](https://github.com/Tea-Cup/ConsoleMenu/releases) and install it with a NuGet console:
```
Install-Package xxx\ConsoleMenu.x.x.x.nupkg
```
Or rip `.dll` from inside the `.nupkg` file manually. NuGet packages can be opened as a ZIP archive. DLL file and its XML-doc can be found in `lib` directory there.
