namespace MyFirstBlog.Dtos;

public record PostDto {
    public Guid Id { get; init; }
    public string Title { get; init; } = default!;
    public string Slug { get; init; } = default!;
    public string Body { get; init; } = default!;
    public string Description { get; set; } = default!;
    public DateTime CreatedDate { get; init; }
}
