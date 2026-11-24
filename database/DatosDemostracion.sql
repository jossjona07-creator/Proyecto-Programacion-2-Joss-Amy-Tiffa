-- Datos de ejemplo de citas atendidas y canceladas.
-- No borra datos ni repite estas citas.
USE AgendaMedicaDB;
START TRANSACTION;

-- Agrega solo los pacientes que falten.
INSERT IGNORE INTO pacientes (Identificacion, Nombre) VALUES
('DEMO-AGENDA-JASLYN', 'Jaslyn'),
('DEMO-AGENDA-JASON', 'Jason'),
('DEMO-AGENDA-AMY', 'Amy');
INSERT INTO pacientes (Identificacion, Nombre)
SELECT 'DEMO-AGENDA-JOAN', 'Joan'
WHERE NOT EXISTS (SELECT 1 FROM pacientes WHERE LOWER(Nombre) LIKE 'joan%');

SET @idJaslyn = (SELECT IdPaciente FROM pacientes WHERE Identificacion='DEMO-AGENDA-JASLYN');
SET @idJoan = (SELECT IdPaciente FROM pacientes WHERE LOWER(Nombre) LIKE 'joan%' ORDER BY IdPaciente LIMIT 1);
SET @idJason = (SELECT IdPaciente FROM pacientes WHERE Identificacion='DEMO-AGENDA-JASON');
SET @idAmy = (SELECT IdPaciente FROM pacientes WHERE Identificacion='DEMO-AGENDA-AMY');
-- Busca un médico activo con horario suficiente.
SET @idMedico = (SELECT IdMedico FROM medicos
  WHERE Estado='Activo'
    AND HorarioAtencion REGEXP '^([01][0-9]|2[0-3]):[0-5][0-9]-([01][0-9]|2[0-3]):[0-5][0-9]$'
    AND SUBSTRING(HorarioAtencion,1,5)<='10:00'
    AND SUBSTRING(HorarioAtencion,7,5)>='14:30'
  ORDER BY IdMedico LIMIT 1);

-- Si no hay un médico con ese horario, no agrega las citas.
INSERT INTO citas (IdPaciente,IdMedico,FechaHora,DuracionMinutos,TipoCita,EstadoCita,Motivo)
SELECT @idJaslyn,@idMedico,'2026-11-17 10:00:00',30,'Presencial','Atendida','Control de demostración'
WHERE @idJaslyn IS NOT NULL AND @idMedico IS NOT NULL
  AND NOT EXISTS (SELECT 1 FROM citas WHERE IdPaciente=@idJaslyn AND FechaHora='2026-11-17 10:00:00');
INSERT INTO citas (IdPaciente,IdMedico,FechaHora,DuracionMinutos,TipoCita,EstadoCita,Motivo)
SELECT @idJoan,@idMedico,'2026-11-18 11:00:00',30,'Presencial','Atendida','Control de demostración'
WHERE @idJoan IS NOT NULL AND @idMedico IS NOT NULL
  AND NOT EXISTS (SELECT 1 FROM citas WHERE IdPaciente=@idJoan AND FechaHora='2026-11-18 11:00:00');
INSERT INTO citas (IdPaciente,IdMedico,FechaHora,DuracionMinutos,TipoCita,EstadoCita,Motivo)
SELECT @idJason,@idMedico,'2026-11-19 13:00:00',30,'Presencial','Cancelada','Cita cancelada de demostración'
WHERE @idJason IS NOT NULL AND @idMedico IS NOT NULL
  AND NOT EXISTS (SELECT 1 FROM citas WHERE IdPaciente=@idJason AND FechaHora='2026-11-19 13:00:00');
INSERT INTO citas (IdPaciente,IdMedico,FechaHora,DuracionMinutos,TipoCita,EstadoCita,Motivo)
SELECT @idAmy,@idMedico,'2026-11-20 14:00:00',30,'Virtual','Cancelada','Cita cancelada de demostración'
WHERE @idAmy IS NOT NULL AND @idMedico IS NOT NULL
  AND NOT EXISTS (SELECT 1 FROM citas WHERE IdPaciente=@idAmy AND FechaHora='2026-11-20 14:00:00');

COMMIT;
SELECT p.Nombre,c.FechaHora,c.EstadoCita,m.Nombre AS Medico
FROM citas c JOIN pacientes p ON p.IdPaciente=c.IdPaciente
JOIN medicos m ON m.IdMedico=c.IdMedico
WHERE c.Motivo LIKE '%demostración%'
ORDER BY c.FechaHora;
