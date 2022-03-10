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
