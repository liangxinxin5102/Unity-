var MyPlugin = {
    Hello: function(str)
    {
        console.log(Pointer_stringify(str));
		SendMessage('Main Camera', 'onSayHello', '我是javascript');
    },
	Other: function()
    {
    }
};

mergeInto(LibraryManager.library, MyPlugin);