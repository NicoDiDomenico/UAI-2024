import { NestFactory } from '@nestjs/core';
import { AppModule } from '../../../app.module';
import { ActionsService } from '@modules/actions/actions.service';
import { GroupsService } from '@modules/groups/groups.service';
import { UsersService } from '@modules/users/users.service';

/**
 * Script para ejecutar todos los seeders
 * Orden: Actions → Groups → Users
 */
async function runSeeds() {
  console.log('🌱 Starting database seeding...\n');

  const app = await NestFactory.createApplicationContext(AppModule);

  try {
    // 1. Seed de Actions
    const actionsService = app.get(ActionsService);
    await actionsService.seed();
    console.log('');

    // 2. Seed de Groups
    const groupsService = app.get(GroupsService);
    await groupsService.seed();
    console.log('');

    // 3. Seed de Users (incluye asignación de grupos)
    const usersService = app.get(UsersService);
    await usersService.seed();
    console.log('');

    console.log('✅ Database seeding completed successfully!\n');
  } catch (error) {
    console.error('❌ Error during seeding:', error);
    process.exit(1);
  } finally {
    await app.close();
  }
}

runSeeds();
