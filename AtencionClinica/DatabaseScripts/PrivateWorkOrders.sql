SELECT PrivateWorkOrderDetails.ProductId, Products.Name, 
PrivateWorkOrders.Date, PrivateWorkOrders.DoctorId, FollowsPrivates.AreaTargetId, 
Areas.Name AS Expr1, Doctors.MinsaCode, PrivateWorkOrders.Active, PrivateWorkOrders.CreateAt, PrivateWorkOrderDetails.Price, PrivateWorkOrderDetails.Costo
FROM     PrivateWorkOrders INNER JOIN
PrivateWorkOrderDetails on PrivateWorkOrders.Id = PrivateWorkOrderDetails.PrivateWorkOrderId INNER JOIN
--PrivateWorkPreOrderDetails ON PrivateWorkOrders.Id = PrivateWorkPreOrderDetails.PrivateWorkOrderId INNER JOIN
Products ON PrivateWorkOrderDetails.ProductId = Products.Id INNER JOIN
FollowsPrivates ON PrivateWorkOrders.FollowsPrivateId = FollowsPrivates.Id INNER JOIN
Bills ON FollowsPrivates.BillId = Bills.Id INNER JOIN
PrivateCustomers ON Bills.PrivateCustomerId = PrivateCustomers.Id INNER JOIN
Doctors ON PrivateWorkOrders.DoctorId = Doctors.Id left JOIN
Areas ON FollowsPrivates.AreaTargetId = Areas.Id
ORDER BY CreateAt desc