alter procedure GetCartDetailsbyCustomer
(
@cid int
)

as

select pd.CategoryId, 
pd.ModelNumber,
pd.ModelName, 
pd.ProductImage,
pd.UnitCost,
pd.Description,
sc.CartId,
sc.Quantity
from ShoppingCart Sc 
inner join Products pd  on sc.ProductId =pd.ProductId
where sc.CustomerID = @cid

--exec GetCartDetailsbyCustomer 1