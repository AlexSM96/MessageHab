using MessageHub.Clients.Pages;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{ 
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.MapRazorComponents<MessageSenderModel>();
app.MapRazorComponents<MessageReaderModel>();
app.MapRazorComponents<MessageHistoryModel>();
app.Run();
