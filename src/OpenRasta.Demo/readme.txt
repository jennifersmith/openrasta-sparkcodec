The demo references OR dlls from ..\bin\Release\net-35 although you can copy OpenRasta.dll & OpenRasta.Codecs.SharpView.dll to the bin & be done.

The demo needs you to patch your local copy of OR with http://trac.caffeine-it.com/openrasta/ticket/65 for HttpMethodOverrideUriDecorator to work with form helpers. Otherwise it'll throw when browsing /widgets

ProductHandler.PostImage action desn't work for now.