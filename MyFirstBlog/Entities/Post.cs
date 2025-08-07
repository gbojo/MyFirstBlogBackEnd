namespace MyFirstBlog.Entities;
public record Post {
    public Guid Id { get; init; }
    public string Title { get; init; } = default!;
    public string Slug { get; init; } = default!;
    public string Body { get; init; } = default!;
    public string Description { get; set; }
    public DateTime CreatedDate { get; init; }
}
