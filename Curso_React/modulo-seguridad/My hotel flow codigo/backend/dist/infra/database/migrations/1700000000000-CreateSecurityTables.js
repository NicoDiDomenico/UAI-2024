"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.CreateSecurityTables1700000000000 = void 0;
class CreateSecurityTables1700000000000 {
    name = 'CreateSecurityTables1700000000000';
    async up(queryRunner) {
        await queryRunner.query(`
      CREATE TABLE "action" (
        "id" SERIAL PRIMARY KEY,
        "key" VARCHAR(100) NOT NULL UNIQUE,
        "name" VARCHAR(255) NOT NULL,
        "description" TEXT,
        "area" VARCHAR(50),
        "createdAt" TIMESTAMP NOT NULL DEFAULT now(),
        "updatedAt" TIMESTAMP NOT NULL DEFAULT now()
      )
    `);
        await queryRunner.query(`
      CREATE TABLE "group" (
        "id" SERIAL PRIMARY KEY,
        "key" VARCHAR(100) NOT NULL UNIQUE,
        "name" VARCHAR(255) NOT NULL,
        "description" TEXT,
        "createdAt" TIMESTAMP NOT NULL DEFAULT now(),
        "updatedAt" TIMESTAMP NOT NULL DEFAULT now()
      )
    `);
        await queryRunner.query(`
      CREATE TABLE "user" (
        "id" SERIAL PRIMARY KEY,
        "username" VARCHAR(50) NOT NULL UNIQUE,
        "email" VARCHAR(255) NOT NULL UNIQUE,
        "passwordHash" VARCHAR(255) NOT NULL,
        "fullName" VARCHAR(255),
        "isActive" BOOLEAN NOT NULL DEFAULT true,
        "lastLoginAt" TIMESTAMP,
        "failedLoginAttempts" INTEGER NOT NULL DEFAULT 0,
        "lockedUntil" TIMESTAMP,
        "passwordResetToken" VARCHAR(255),
        "passwordResetExpires" TIMESTAMP,
        "createdAt" TIMESTAMP NOT NULL DEFAULT now(),
        "updatedAt" TIMESTAMP NOT NULL DEFAULT now()
      )
    `);
        await queryRunner.query(`
      CREATE TABLE "revoked_token" (
        "id" SERIAL PRIMARY KEY,
        "jti" VARCHAR(255) NOT NULL UNIQUE,
        "userId" INTEGER NOT NULL,
        "tokenType" VARCHAR(20) NOT NULL,
        "reason" VARCHAR(255) NOT NULL,
        "ip" VARCHAR(50),
        "expiresAt" TIMESTAMP NOT NULL,
        "createdAt" TIMESTAMP NOT NULL DEFAULT now()
      )
    `);
        await queryRunner.query(`
      CREATE TABLE "audit_log" (
        "id" SERIAL PRIMARY KEY,
        "userId" INTEGER,
        "action" VARCHAR(255) NOT NULL,
        "entity" VARCHAR(100),
        "entityId" VARCHAR(100),
        "metadata" JSONB,
        "ip" VARCHAR(50),
        "userAgent" TEXT,
        "createdAt" TIMESTAMP NOT NULL DEFAULT now()
      )
    `);
        await queryRunner.query(`
      CREATE TABLE "group_actions" (
        "group_id" INTEGER NOT NULL,
        "action_id" INTEGER NOT NULL,
        PRIMARY KEY ("group_id", "action_id"),
        CONSTRAINT "FK_group_actions_group" FOREIGN KEY ("group_id")
          REFERENCES "group"("id") ON DELETE CASCADE,
        CONSTRAINT "FK_group_actions_action" FOREIGN KEY ("action_id")
          REFERENCES "action"("id") ON DELETE CASCADE
      )
    `);
        await queryRunner.query(`
      CREATE TABLE "group_children" (
        "parent_group_id" INTEGER NOT NULL,
        "child_group_id" INTEGER NOT NULL,
        PRIMARY KEY ("parent_group_id", "child_group_id"),
        CONSTRAINT "FK_group_children_parent" FOREIGN KEY ("parent_group_id")
          REFERENCES "group"("id") ON DELETE CASCADE,
        CONSTRAINT "FK_group_children_child" FOREIGN KEY ("child_group_id")
          REFERENCES "group"("id") ON DELETE CASCADE
      )
    `);
        await queryRunner.query(`
      CREATE TABLE "user_groups" (
        "user_id" INTEGER NOT NULL,
        "group_id" INTEGER NOT NULL,
        PRIMARY KEY ("user_id", "group_id"),
        CONSTRAINT "FK_user_groups_user" FOREIGN KEY ("user_id")
          REFERENCES "user"("id") ON DELETE CASCADE,
        CONSTRAINT "FK_user_groups_group" FOREIGN KEY ("group_id")
          REFERENCES "group"("id") ON DELETE CASCADE
      )
    `);
        await queryRunner.query(`
      CREATE TABLE "user_actions" (
        "user_id" INTEGER NOT NULL,
        "action_id" INTEGER NOT NULL,
        PRIMARY KEY ("user_id", "action_id"),
        CONSTRAINT "FK_user_actions_user" FOREIGN KEY ("user_id")
          REFERENCES "user"("id") ON DELETE CASCADE,
        CONSTRAINT "FK_user_actions_action" FOREIGN KEY ("action_id")
          REFERENCES "action"("id") ON DELETE CASCADE
      )
    `);
        await queryRunner.query(`CREATE INDEX "IDX_action_key" ON "action"("key")`);
        await queryRunner.query(`CREATE INDEX "IDX_action_area" ON "action"("area")`);
        await queryRunner.query(`CREATE INDEX "IDX_group_key" ON "group"("key")`);
        await queryRunner.query(`CREATE INDEX "IDX_user_email" ON "user"("email")`);
        await queryRunner.query(`CREATE INDEX "IDX_user_username" ON "user"("username")`);
        await queryRunner.query(`CREATE INDEX "IDX_user_isActive" ON "user"("isActive")`);
        await queryRunner.query(`CREATE INDEX "IDX_revoked_token_jti" ON "revoked_token"("jti")`);
        await queryRunner.query(`CREATE INDEX "IDX_revoked_token_expiresAt" ON "revoked_token"("expiresAt")`);
        await queryRunner.query(`CREATE INDEX "IDX_audit_log_userId" ON "audit_log"("userId")`);
        await queryRunner.query(`CREATE INDEX "IDX_audit_log_action" ON "audit_log"("action")`);
        await queryRunner.query(`CREATE INDEX "IDX_audit_log_createdAt" ON "audit_log"("createdAt")`);
    }
    async down(queryRunner) {
        await queryRunner.query(`DROP TABLE IF EXISTS "user_actions"`);
        await queryRunner.query(`DROP TABLE IF EXISTS "user_groups"`);
        await queryRunner.query(`DROP TABLE IF EXISTS "group_children"`);
        await queryRunner.query(`DROP TABLE IF EXISTS "group_actions"`);
        await queryRunner.query(`DROP TABLE IF EXISTS "audit_log"`);
        await queryRunner.query(`DROP TABLE IF EXISTS "revoked_token"`);
        await queryRunner.query(`DROP TABLE IF EXISTS "user"`);
        await queryRunner.query(`DROP TABLE IF EXISTS "group"`);
        await queryRunner.query(`DROP TABLE IF EXISTS "action"`);
    }
}
exports.CreateSecurityTables1700000000000 = CreateSecurityTables1700000000000;
//# sourceMappingURL=1700000000000-CreateSecurityTables.js.map