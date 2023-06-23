namespace Rafsan.DataAccess.Repositories;

public interface IEntity<TId>
{
	TId Id { get; set; }
}

public interface IEntity : IEntity<string>
{

}
