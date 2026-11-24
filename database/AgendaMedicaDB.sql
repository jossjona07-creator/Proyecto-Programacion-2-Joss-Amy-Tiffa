CREATE DATABASE IF NOT EXISTS AgendaMedicaDB CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE AgendaMedicaDB;

CREATE TABLE IF NOT EXISTS pacientes (
  IdPaciente INT NOT NULL AUTO_INCREMENT,
  Identificacion VARCHAR(30) NOT NULL,
  Nombre VARCHAR(150) NOT NULL,
  Telefono VARCHAR(30) NULL,
  Correo VARCHAR(150) NULL,
  FechaNacimiento DATE NULL,
  PRIMARY KEY (IdPaciente), UNIQUE KEY uq_pacientes_identificacion (Identificacion)
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS medicos (
  IdMedico INT NOT NULL AUTO_INCREMENT,
  Identificacion VARCHAR(30) NOT NULL,
  Nombre VARCHAR(150) NOT NULL,
  Telefono VARCHAR(30) NULL,
  Correo VARCHAR(150) NULL,
  HorarioAtencion VARCHAR(11) NOT NULL COMMENT 'Formato HH:mm-HH:mm, por ejemplo 08:00-16:00',
  Estado VARCHAR(20) NOT NULL DEFAULT 'Activo',
  PRIMARY KEY (IdMedico), UNIQUE KEY uq_medicos_identificacion (Identificacion),
  CONSTRAINT chk_medicos_estado CHECK (Estado IN ('Activo','Inactivo'))
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS especialidades (
  IdEspecialidad INT NOT NULL AUTO_INCREMENT,
  NombreEspecialidad VARCHAR(100) NOT NULL,
  Descripcion VARCHAR(500) NULL,
  PRIMARY KEY (IdEspecialidad), UNIQUE KEY uq_especialidades_nombre (NombreEspecialidad)
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS medicoespecialidad (
  IdMedico INT NOT NULL,
  IdEspecialidad INT NOT NULL,
  PRIMARY KEY (IdMedico, IdEspecialidad),
  CONSTRAINT fk_medicoespecialidad_medico FOREIGN KEY (IdMedico) REFERENCES medicos(IdMedico) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT fk_medicoespecialidad_especialidad FOREIGN KEY (IdEspecialidad) REFERENCES especialidades(IdEspecialidad) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS citas (
  IdCita INT NOT NULL AUTO_INCREMENT,
  IdPaciente INT NOT NULL,
  IdMedico INT NOT NULL,
  FechaHora DATETIME NOT NULL,
  DuracionMinutos INT NOT NULL,
  TipoCita VARCHAR(20) NOT NULL,
  EstadoCita VARCHAR(25) NOT NULL DEFAULT 'Programada',
  Motivo VARCHAR(500) NULL,
  PRIMARY KEY (IdCita),
  KEY ix_citas_medico_fecha (IdMedico, FechaHora), KEY ix_citas_paciente_fecha (IdPaciente, FechaHora), KEY ix_citas_estado_fecha (EstadoCita, FechaHora),
  CONSTRAINT fk_citas_paciente FOREIGN KEY (IdPaciente) REFERENCES pacientes(IdPaciente) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT fk_citas_medico FOREIGN KEY (IdMedico) REFERENCES medicos(IdMedico) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT chk_citas_duracion CHECK (DuracionMinutos IN (30,45,60)),
  CONSTRAINT chk_citas_tipo CHECK (TipoCita IN ('Presencial','Virtual','Urgencia')),
  CONSTRAINT chk_citas_estado CHECK (EstadoCita IN ('Programada','Confirmada','Atendida','Cancelada','No asistió','Reprogramada'))
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS historialmedico (
  IdHistorial INT NOT NULL AUTO_INCREMENT,
  IdPaciente INT NOT NULL,
  IdMedico INT NOT NULL,
  FechaRegistro DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  Diagnostico VARCHAR(1000) NOT NULL,
  Tratamiento VARCHAR(1000) NULL,
  Observaciones VARCHAR(2000) NULL,
  PRIMARY KEY (IdHistorial), KEY ix_historial_paciente_fecha (IdPaciente, FechaRegistro),
  CONSTRAINT fk_historial_paciente FOREIGN KEY (IdPaciente) REFERENCES pacientes(IdPaciente) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT fk_historial_medico FOREIGN KEY (IdMedico) REFERENCES medicos(IdMedico) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS roles (
  IdRol INT NOT NULL AUTO_INCREMENT,
  NombreRol VARCHAR(30) NOT NULL,
  PRIMARY KEY (IdRol), UNIQUE KEY uq_roles_nombre (NombreRol)
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS usuarios (
  IdUsuario INT NOT NULL AUTO_INCREMENT,
  IdRol INT NOT NULL,
  NombreUsuario VARCHAR(60) NOT NULL,
  ContrasenaHash VARCHAR(255) NOT NULL,
  Activo TINYINT(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (IdUsuario), UNIQUE KEY uq_usuarios_nombre (NombreUsuario),
  CONSTRAINT fk_usuarios_rol FOREIGN KEY (IdRol) REFERENCES roles(IdRol) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB;

INSERT IGNORE INTO roles (NombreRol) VALUES ('Administrador'), ('Recepción');