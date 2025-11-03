"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const core_1 = require("@nestjs/core");
const app_module_1 = require("../../../app.module");
const actions_service_1 = require("../../../modules/actions/actions.service");
const groups_service_1 = require("../../../modules/groups/groups.service");
const users_service_1 = require("../../../modules/users/users.service");
async function runSeeds() {
    console.log('🌱 Starting database seeding...\n');
    const app = await core_1.NestFactory.createApplicationContext(app_module_1.AppModule);
    try {
        const actionsService = app.get(actions_service_1.ActionsService);
        await actionsService.seed();
        console.log('');
        const groupsService = app.get(groups_service_1.GroupsService);
        await groupsService.seed();
        console.log('');
        const usersService = app.get(users_service_1.UsersService);
        await usersService.seed();
        console.log('');
        console.log('✅ Database seeding completed successfully!\n');
    }
    catch (error) {
        console.error('❌ Error during seeding:', error);
        process.exit(1);
    }
    finally {
        await app.close();
    }
}
runSeeds();
//# sourceMappingURL=run-seeds.js.map