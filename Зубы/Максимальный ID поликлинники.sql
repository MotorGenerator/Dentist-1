select ИНН, COUNT(ИНН), MAX(id_поликлинника) from Поликлинника
group by ИНН

select * from Поликлинника
where ИНН = '6404001219'