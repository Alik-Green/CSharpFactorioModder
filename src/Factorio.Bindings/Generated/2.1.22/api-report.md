# Factorio 2.1.22 API binding report

* api_version: 6 (stage `runtime`)
* classes: 157, methods+global functions: 1044, attributes: 2773, globals: 9, define leaves: 1414, events: 229, generated table classes: 666, files: 1569

## error (0)

_none_

## skipped (0)

_none_

## degraded (734)

* [runtime] `LuaBootstrap::feature_flags` - anonymous table type mapped to LuaTable<string, object>  
  _table{expansion,expansion_shaders,freezing,quality,rail_bridges,segmented_units,space_travel,spoiling}_
* [runtime] `LuaBootstrap::level` - anonymous table type mapped to LuaTable<string, object>  
  _table{campaign_name,is_simulation,is_tutorial,level_name,mod_name}_
* [runtime] `LuaBootstrap::get_event_filter::return` - union collapsed to object  
  _(LuaSegmentedUnitDiedEventFilter|LuaScriptRaisedTeleportedEventFilter|LuaPreRobotMinedEntityEventFilter|LuaScriptRaisedBuiltEventFilter|LuaPlatformMinedEntityEventFilter|LuaRobotBuiltEntityEventFilter|LuaPrePlayerMinedEntityEventFilter|LuaEntityDeconstructionCancelledEventFilter|LuaPreGhostUpgradedEventFilter|LuaPlatformBuiltEntityEventFilter|LuaPrePlatformMinedEntityEventFilter|LuaEntityClonedEventFilter|LuaPlayerRepairedEntityEventFilter|LuaPostEntityDiedEventFilter|LuaScriptRaisedDestroySegmentedUnitEventFilter|LuaSegmentedUnitDamagedEventFilter|LuaPreGhostDeconstructedEventFilter|LuaPlayerMinedEntityEventFilter|LuaSectorScannedEventFilter|LuaRobotMinedEntityEventFilter|LuaEntityMarkedForDeconstructionEventFilter|LuaPostSegmentedUnitDiedEventFilter|LuaScriptRaisedReviveEventFilter|LuaPlayerBuiltEntityEventFilter|LuaUpgradeCancelledEventFilter|LuaEntityDamagedEventFilter|LuaEntityDiedEventFilter|LuaEntityMarkedForUpgradeEventFilter|LuaSegmentedUnitCreatedEventFilter|LuaScriptRaisedDestroyEventFilter)_
* [runtime] `LuaBootstrap::on_event::event` - union collapsed to object  
  _(LuaEventType|array[LuaEventType])_
* [runtime] `LuaBootstrap::on_event::filters` - union collapsed to object  
  _(LuaSegmentedUnitDiedEventFilter|LuaScriptRaisedTeleportedEventFilter|LuaPreRobotMinedEntityEventFilter|LuaScriptRaisedBuiltEventFilter|LuaPlatformMinedEntityEventFilter|LuaRobotBuiltEntityEventFilter|LuaPrePlayerMinedEntityEventFilter|LuaEntityDeconstructionCancelledEventFilter|LuaPreGhostUpgradedEventFilter|LuaPlatformBuiltEntityEventFilter|LuaPrePlatformMinedEntityEventFilter|LuaEntityClonedEventFilter|LuaPlayerRepairedEntityEventFilter|LuaPostEntityDiedEventFilter|LuaScriptRaisedDestroySegmentedUnitEventFilter|LuaSegmentedUnitDamagedEventFilter|LuaPreGhostDeconstructedEventFilter|LuaPlayerMinedEntityEventFilter|LuaSectorScannedEventFilter|LuaRobotMinedEntityEventFilter|LuaEntityMarkedForDeconstructionEventFilter|LuaPostSegmentedUnitDiedEventFilter|LuaScriptRaisedReviveEventFilter|LuaPlayerBuiltEntityEventFilter|LuaUpgradeCancelledEventFilter|LuaEntityDamagedEventFilter|LuaEntityDiedEventFilter|LuaEntityMarkedForUpgradeEventFilter|LuaSegmentedUnitCreatedEventFilter|LuaScriptRaisedDestroyEventFilter)_
* [runtime] `LuaBootstrap::on_nth_tick::tick` - union collapsed to object  
  _(MapTick|array[MapTick]|nil)_
* [runtime] `LuaBootstrap::set_event_filter::filters` - union collapsed to object  
  _(LuaSegmentedUnitDiedEventFilter|LuaScriptRaisedTeleportedEventFilter|LuaPreRobotMinedEntityEventFilter|LuaScriptRaisedBuiltEventFilter|LuaPlatformMinedEntityEventFilter|LuaRobotBuiltEntityEventFilter|LuaPrePlayerMinedEntityEventFilter|LuaEntityDeconstructionCancelledEventFilter|LuaPreGhostUpgradedEventFilter|LuaPlatformBuiltEntityEventFilter|LuaPrePlatformMinedEntityEventFilter|LuaEntityClonedEventFilter|LuaPlayerRepairedEntityEventFilter|LuaPostEntityDiedEventFilter|LuaScriptRaisedDestroySegmentedUnitEventFilter|LuaSegmentedUnitDamagedEventFilter|LuaPreGhostDeconstructedEventFilter|LuaPlayerMinedEntityEventFilter|LuaSectorScannedEventFilter|LuaRobotMinedEntityEventFilter|LuaEntityMarkedForDeconstructionEventFilter|LuaPostSegmentedUnitDiedEventFilter|LuaScriptRaisedReviveEventFilter|LuaPlayerBuiltEntityEventFilter|LuaUpgradeCancelledEventFilter|LuaEntityDamagedEventFilter|LuaEntityDiedEventFilter|LuaEntityMarkedForUpgradeEventFilter|LuaSegmentedUnitCreatedEventFilter|LuaScriptRaisedDestroyEventFilter)_
* [runtime] `LuaBurner::currently_burning` - the documented read type (ItemIDAndQualityIDPair) and write type (object) differ, so the value is exposed as object
* [runtime] `LuaBurner::owner` - union collapsed to object  
  _(LuaEntity|LuaEquipment)_
* [runtime] `LuaBurnerPrototype::light_flicker` - anonymous table type mapped to LuaTable<string, object>  
  _table{border_fix_speed,color,derivation_change_deviation,derivation_change_frequency,light_intensity_to_size_coefficient,maximum_intensity,minimum_intensity,minimum_light_size}_
* [runtime] `LuaCommandable::add_member::member` - union collapsed to object  
  _(LuaCommandable|LuaEntity)_
* [runtime] `LuaControl::cursor_ghost` - the documented read type (ItemIDAndQualityIDPair) and write type (object) differ, so the value is exposed as object
* [runtime] `LuaControl::force` - the documented read type (Force) and write type (object) differ, so the value is exposed as object
* [runtime] `LuaControl::mining_state` - anonymous table type mapped to LuaTable<string, object>  
  _table{mining,position}_
* [runtime] `LuaControl::mining_state` - anonymous table type mapped to LuaTable<string, object>  
  _table{mining,position}_
* [runtime] `LuaControl::opened` - union collapsed to object  
  _(LuaEntity|LuaItemStack|LuaEquipment|LuaEquipmentGrid|LuaPlayer|LuaGuiElement|LuaInventory|LuaLogisticNetwork|LuaTile|defines.gui_type)_
* [runtime] `LuaControl::opened` - union collapsed to object  
  _(LuaEntity|LuaItemStack|LuaEquipment|LuaEquipmentGrid|LuaPlayer|LuaGuiElement|LuaInventory|LuaLogisticNetwork|LuaTile|defines.gui_type)_
* [runtime] `LuaControl::repair_state` - anonymous table type mapped to LuaTable<string, object>  
  _table{position,repairing}_
* [runtime] `LuaControl::repair_state` - anonymous table type mapped to LuaTable<string, object>  
  _table{position,repairing}_
* [runtime] `LuaControl::shooting_state` - anonymous table type mapped to LuaTable<string, object>  
  _table{position,state}_
* [runtime] `LuaControl::shooting_state` - anonymous table type mapped to LuaTable<string, object>  
  _table{position,state}_
* [runtime] `LuaControl::walking_state` - anonymous table type mapped to LuaTable<string, object>  
  _table{direction,walking}_
* [runtime] `LuaControl::walking_state` - anonymous table type mapped to LuaTable<string, object>  
  _table{direction,walking}_
* [runtime] `LuaCustomChartTag::last_user` - the documented read type (Player) and write type (object) differ, so the value is exposed as object
* [runtime] `LuaElectricNetwork::flow_last_tick` - anonymous table type mapped to LuaTable<string, object>  
  _table{accumulator_capacity,accumulator_energy,consumption_satisfaction,lamp_satisfaction,maximum_consumption,maximum_production,primary_demand,primary_demand_usage,primary_output,primary_output_usage,production_satisfaction,secondary_demand,secondary_demand_usage,secondary_output,secondary_output_usage,solar_output,solar_output_usage,tertiary_demand,tertiary_demand_usage,tertiary_output,tertiary_output_usage,total_transfer}_
* [runtime] `LuaEntity::associated_player` - the documented read type (Player) and write type (object) differ, so the value is exposed as object
* [runtime] `LuaEntity::belt_neighbours` - anonymous table type mapped to LuaTable<string, object>  
  _table{inputs,outputs}_
* [runtime] `LuaEntity::cliff_neighbours` - anonymous table type mapped to LuaTable<string, object>  
  _table{east,north,south,west}_
* [runtime] `LuaEntity::ghost_prototype` - union collapsed to object  
  _(LuaEntityPrototype|LuaTilePrototype)_
* [runtime] `LuaEntity::last_user` - the documented read type (Player) and write type (object) differ, so the value is exposed as object
* [runtime] `LuaEntity::render_player` - the documented read type (Player) and write type (object) differ, so the value is exposed as object
* [runtime] `LuaEntity::render_to_forces` - the documented read type (LuaArray<Force>) and write type (object) differ, so the value is exposed as object
* [runtime] `LuaEntity::result_quality` - the documented read type (QualityPrototype) and write type (object) differ, so the value is exposed as object
* [runtime] `LuaEntity::sticker_vehicle_modifiers` - anonymous table type mapped to LuaTable<string, object>  
  _table{friction_modifier,speed_max,speed_modifier}_
* [runtime] `LuaEntity::storage_filter` - the documented read type (ItemIDAndQualityIDPair) and write type (object) differ, so the value is exposed as object
* [runtime] `LuaEntity::wall_neighbours` - anonymous table type mapped to LuaTable<string, object>  
  _table{east,north,south,west}_
* [runtime] `LuaEntity::get_driver::return` - union collapsed to object  
  _(LuaEntity|LuaPlayer)_
* [runtime] `LuaEntity::get_filter::return` - union collapsed to object  
  _(ItemFilter|EntityID|AsteroidChunkID)_
* [runtime] `LuaEntity::get_fluid_box_prototype::return` - union collapsed to object  
  _(LuaFluidBoxPrototype|array[LuaFluidBoxPrototype])_
* [runtime] `LuaEntity::get_logistic_point::return` - union collapsed to object  
  _(LuaLogisticPoint|array[LuaLogisticPoint])_
* [runtime] `LuaEntity::get_passenger::return` - union collapsed to object  
  _(LuaEntity|LuaPlayer)_
* [runtime] `LuaEntity::set_beam_source::source` - union collapsed to object  
  _(LuaEntity|MapPosition)_
* [runtime] `LuaEntity::set_beam_target::target` - union collapsed to object  
  _(LuaEntity|MapPosition)_
* [runtime] `LuaEntity::set_driver::driver` - union collapsed to object  
  _(LuaEntity|PlayerIdentification|nil)_
* [runtime] `LuaEntity::set_filter::filter` - union collapsed to object  
  _(ItemFilter|ItemWithQualityID|EntityID|AsteroidChunkID)_
* [runtime] `LuaEntity::set_passenger::passenger` - union collapsed to object  
  _(LuaEntity|PlayerIdentification|nil)_
* [runtime] `LuaEntityPrototype::created_smoke` - anonymous table type mapped to LuaTable<string, object>  
  _table{initial_height,max_radius,offset_deviation,offsets,smoke_name,speed,speed_from_center,speed_from_center_deviation,speed_multiplier,speed_multiplier_deviation,starting_frame,starting_frame_deviation}_
* [runtime] `LuaEntityPrototype::logistic_parameters` - anonymous table type mapped to LuaTable<string, object>  
  _table{charge_approach_distance,charging_distance,charging_energy,charging_station_count,charging_station_shift,charging_threshold_distance,construction_radius,logistic_radius,logistics_connection_distance,robot_limit,robot_vertical_acceleration,robots_shrink_when_entering_and_exiting,spawn_and_station_height,spawn_and_station_shadow_height_offset,stationing_offset,stationing_render_layer_swap_height}_
* [runtime] `LuaEntityPrototype::spawn_cooldown` - anonymous table type mapped to LuaTable<string, object>  
  _table{max,min}_
* [runtime] `LuaEntityPrototype::sticker_movement_modifiers` - anonymous table type mapped to LuaTable<string, object>  
  _table{target_movement_max_from,target_movement_max_to,target_movement_modifier_from,target_movement_modifier_to}_
* [runtime] `LuaEntityPrototype::sticker_vehicle_modifiers` - anonymous table type mapped to LuaTable<string, object>  
  _table{vehicle_friction_modifier_from,vehicle_friction_modifier_to,vehicle_speed_max_from,vehicle_speed_max_to,vehicle_speed_modifier_from,vehicle_speed_modifier_to}_
* [runtime] `LuaEquipment::shape` - anonymous table type mapped to LuaTable<string, object>  
  _table{height,width}_
* [runtime] `LuaEquipmentPrototype::logistic_parameters` - anonymous table type mapped to LuaTable<string, object>  
  _table{charge_approach_distance,charging_distance,charging_energy,charging_station_count,charging_station_shift,charging_threshold_distance,construction_radius,logistic_radius,logistics_connection_distance,robot_limit,robot_vertical_acceleration,robots_shrink_when_entering_and_exiting,spawn_and_station_height,spawn_and_station_shadow_height_offset,stationing_offset,stationing_render_layer_swap_height}_
* [runtime] `LuaEquipmentPrototype::shape` - anonymous table type mapped to LuaTable<string, object>  
  _table{height,points,width}_
* [runtime] `LuaGameScript::ban_player::player` - union collapsed to object  
  _(PlayerIdentification|string)_
* [runtime] `LuaGameScript::regenerate_entity::entities` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGameScript::unban_player::player` - union collapsed to object  
  _(PlayerIdentification|string)_
* [runtime] `LuaGuiElement::elem_filters` - union collapsed to object  
  _(ModSettingPrototypeFilter|SpaceLocationPrototypeFilter|DecorativePrototypeFilter|TilePrototypeFilter|AsteroidChunkPrototypeFilter|ItemPrototypeFilter|TechnologyPrototypeFilter|RecipePrototypeFilter|AchievementPrototypeFilter|VirtualSignalPrototypeFilter|EquipmentPrototypeFilter|FluidPrototypeFilter|EntityPrototypeFilter)_
* [runtime] `LuaGuiElement::elem_filters` - union collapsed to object  
  _(ModSettingPrototypeFilter|SpaceLocationPrototypeFilter|DecorativePrototypeFilter|TilePrototypeFilter|AsteroidChunkPrototypeFilter|ItemPrototypeFilter|TechnologyPrototypeFilter|RecipePrototypeFilter|AchievementPrototypeFilter|VirtualSignalPrototypeFilter|EquipmentPrototypeFilter|FluidPrototypeFilter|EntityPrototypeFilter)_
* [runtime] `LuaGuiElement::elem_value` - union collapsed to object  
  _(string|SignalID|PrototypeWithQuality)_
* [runtime] `LuaGuiElement::elem_value` - union collapsed to object  
  _(string|SignalID|PrototypeWithQuality)_
* [runtime] `LuaGuiElement::mouse_button_filter` - the documented read type (LuaTable<string, bool>) and write type (LuaArray<string>) differ, so the value is exposed as object
* [runtime] `LuaGuiElement::quality` - the documented read type (QualityPrototype) and write type (object) differ, so the value is exposed as object
* [runtime] `LuaGuiElement::style` - union collapsed to object  
  _(LuaStyle|string)_
* [runtime] `LuaGuiElement::style` - union collapsed to object  
  _(LuaStyle|string)_
* [runtime] `LuaItemCommon::get_mapper::return` - union collapsed to object  
  _(UpgradeMapperSource|UpgradeMapperDestination)_
* [runtime] `LuaItemCommon::set_mapper::mapper` - union collapsed to object  
  _(UpgradeMapperSource|UpgradeMapperDestination|nil)_
* [runtime] `LuaItemCommon::set_tile_filter::filter` - union collapsed to object  
  _(string|LuaTilePrototype|LuaTile|nil)_
* [runtime] `LuaModSettingPrototype::allowed_values` - union collapsed to object  
  _(array[string]|array[int32]|array[double])_
* [runtime] `LuaModSettingPrototype::default_value` - union collapsed to object  
  _(boolean|double|int32|string|Color)_
* [runtime] `LuaPlayer::respawn_quality` - the documented read type (QualityPrototype) and write type (object) differ, so the value is exposed as object
* [runtime] `LuaPlayer::set_quick_bar_slot::filter` - union collapsed to object  
  _(LuaItemStack|ItemWithQualityID|QuickBarSlot|nil)_
* [runtime] `LuaRecord::get_mapper::return` - union collapsed to object  
  _(UpgradeMapperSource|UpgradeMapperDestination)_
* [runtime] `LuaRecord::set_mapper::mapper` - union collapsed to object  
  _(UpgradeMapperSource|UpgradeMapperDestination|nil)_
* [runtime] `LuaRecord::set_tile_filter::filter` - union collapsed to object  
  _(string|LuaTilePrototype|LuaTile)_
* [runtime] `LuaRenderObject::forces` - the documented read type (LuaArray<Force>) and write type (object) differ, so the value is exposed as object
* [runtime] `LuaRenderObject::players` - union collapsed to object  
  _(array[LuaPlayer]|array[PlayerIdentification])_
* [runtime] `LuaRenderObject::players` - union collapsed to object  
  _(array[LuaPlayer]|array[PlayerIdentification])_
* [runtime] `LuaSchedule::owner` - union collapsed to object  
  _(LuaTrain|LuaSpacePlatform)_
* [runtime] `LuaSegmentedUnit::force` - the documented read type (Force) and write type (object) differ, so the value is exposed as object
* [runtime] `LuaSpacePlatform::damaged_tiles` - anonymous table type mapped to LuaTable<string, object>  
  _table{damage,position}_
* [runtime] `LuaSpacePlatform::space_connection` - the documented read type (SpaceConnectionPrototype) and write type (object) differ, so the value is exposed as object
* [runtime] `LuaSpacePlatform::space_location` - the documented read type (SpaceLocationPrototype) and write type (object) differ, so the value is exposed as object
* [runtime] `LuaStyle::extra_margin_when_activated` - union collapsed to object  
  _(int32|array[int32])_
* [runtime] `LuaStyle::extra_padding_when_activated` - union collapsed to object  
  _(int32|array[int32])_
* [runtime] `LuaStyle::margin` - union collapsed to object  
  _(int16|array[int16])_
* [runtime] `LuaStyle::padding` - union collapsed to object  
  _(int16|array[int16])_
* [runtime] `LuaStyle::size` - union collapsed to object  
  _(int32|array[int32])_
* [runtime] `LuaSurface::daytime_parameters` - anonymous table type mapped to LuaTable<string, object>  
  _table{dawn,dusk,evening,morning}_
* [runtime] `LuaSurface::daytime_parameters` - anonymous table type mapped to LuaTable<string, object>  
  _table{dawn,dusk,evening,morning}_
* [runtime] `LuaSurface::regenerate_decorative::decoratives` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaSurface::regenerate_entity::entities` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaTilePrototype::mineable_properties` - anonymous table type mapped to LuaTable<string, object>  
  _table{minable,mining_particle,mining_time,products}_
* [runtime] `LuaTrain::locomotives` - anonymous table type mapped to LuaTable<string, object>  
  _table{back_movers,front_movers}_
* [runtime] `LuaTrainManager::request_train_path::return` - union collapsed to object  
  _(TrainPathFinderOneGoalResult|TrainPathAllGoalsResult)_
* [runtime] `LuaWireConnector::can_wire_reach::other_connector` - union collapsed to object  
  _(LuaWireConnector|LuaEntity)_
* [runtime] `LuaEntityPrototype::attack_parameters::projectile_creation_parameters` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[RealOrientation,Vector]_
* [runtime] `LuaGameScript::get_vehicles::type` - union collapsed to object  
  _(EntityID|array[EntityID])_
* [runtime] `LuaGuiElement::elem_filters::type` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGuiElement::elem_filters::type` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGuiElement::elem_filters::mask` - union collapsed to object  
  _(CollisionLayerID|CollisionMask)_
* [runtime] `LuaGuiElement::elem_filters::mask` - union collapsed to object  
  _(CollisionLayerID|CollisionMask)_
* [runtime] `LuaGuiElement::elem_filters::name` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGuiElement::elem_filters::type` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGuiElement::elem_filters::type` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGuiElement::elem_filters::type` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGuiElement::elem_filters::name` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGuiElement::elem_filters::name` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGuiElement::elem_filters::type` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGuiElement::elem_filters::mask` - union collapsed to object  
  _(CollisionLayerID|CollisionMask)_
* [runtime] `LuaGuiElement::add::elem_filters` - union collapsed to object  
  _(ModSettingPrototypeFilter|SpaceLocationPrototypeFilter|DecorativePrototypeFilter|TilePrototypeFilter|AsteroidChunkPrototypeFilter|ItemPrototypeFilter|TechnologyPrototypeFilter|RecipePrototypeFilter|AchievementPrototypeFilter|VirtualSignalPrototypeFilter|EquipmentPrototypeFilter|FluidPrototypeFilter|EntityPrototypeFilter)_
* [runtime] `LuaItemCommon::get_blueprint_entities::return::wires` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint32,defines.wire_connector_id,uint32,defines.wire_connector_id]_
* [runtime] `LuaPlayer::mod_settings::value` - union collapsed to object  
  _(int32|double|boolean|string|Color)_
* [runtime] `LuaPlayer::stack_transfers::target` - union collapsed to object  
  _(LuaInventory|LuaEntity|LuaPlayer|LuaItem|LuaItemStack|LuaEquipment)_
* [runtime] `LuaSchedule::change_wait_condition::wait_condition::condition` - union collapsed to object  
  _(CircuitCondition|BlueprintItemIDAndQualityIDPair)_
* [runtime] `LuaSpacePlatform::destroy_asteroid_chunks::name` - union collapsed to object  
  _(AsteroidChunkID|array[AsteroidChunkID])_
* [runtime] `LuaSpacePlatform::find_asteroid_chunks_filtered::name` - union collapsed to object  
  _(AsteroidChunkID|array[AsteroidChunkID])_
* [runtime] `LuaSurface::clone_brush::destination_force` - union collapsed to object  
  _(LuaForce|string)_
* [runtime] `LuaSurface::count_entities_filtered::filter::name` - union collapsed to object  
  _(EntityID|array[EntityID])_
* [runtime] `LuaSurface::count_entities_filtered::filter::type` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaSurface::count_entities_filtered::filter::ghost_name` - union collapsed to object  
  _(EntityID|array[EntityID])_
* [runtime] `LuaSurface::count_entities_filtered::filter::ghost_type` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaSurface::count_entities_filtered::filter::direction` - union collapsed to object  
  _(defines.direction|array[defines.direction])_
* [runtime] `LuaSurface::count_entities_filtered::filter::collision_mask` - union collapsed to object  
  _(CollisionLayerID|array[CollisionLayerID]|dict[CollisionLayerID->literal(boolean:True)])_
* [runtime] `LuaSurface::count_tiles_filtered::filter::name` - union collapsed to object  
  _(TileID|array[TileID])_
* [runtime] `LuaSurface::count_tiles_filtered::filter::collision_mask` - union collapsed to object  
  _(CollisionLayerID|array[CollisionLayerID]|dict[CollisionLayerID->literal(boolean:True)])_
* [runtime] `LuaSurface::create_entity::stack` - union collapsed to object  
  _(string|ItemStackDefinition)_
* [runtime] `LuaSurface::create_entity::source` - union collapsed to object  
  _(LuaEntity|MapPosition)_
* [runtime] `LuaSurface::create_entity::cause` - union collapsed to object  
  _(LuaEntity|ForceID)_
* [runtime] `LuaSurface::destroy_decoratives::name` - union collapsed to object  
  _(DecorativeID|array[DecorativeID])_
* [runtime] `LuaSurface::destroy_decoratives::collision_mask` - union collapsed to object  
  _(CollisionLayerID|array[CollisionLayerID]|dict[CollisionLayerID->literal(boolean:True)])_
* [runtime] `LuaSurface::find_decoratives_filtered::name` - union collapsed to object  
  _(DecorativeID|array[DecorativeID])_
* [runtime] `LuaSurface::find_decoratives_filtered::collision_mask` - union collapsed to object  
  _(CollisionLayerID|array[CollisionLayerID]|dict[CollisionLayerID->literal(boolean:True)])_
* [runtime] `LuaTrainManager::get_train_stops::filter::type` - union collapsed to object  
  _(EntityID|array[EntityID])_
* [runtime] `LuaTrainManager::get_train_stops::filter::station_name` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaTrainManager::get_trains::filter::stock` - union collapsed to object  
  _(EntityID|array[EntityID])_
* [runtime] `LuaEntityPrototype::attack_parameters::projectile_creation_parameters` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[RealOrientation,Vector]_
* [runtime] `LuaGameScript::get_vehicles::type` - union collapsed to object  
  _(EntityID|array[EntityID])_
* [runtime] `LuaGuiElement::elem_filters::type` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGuiElement::elem_filters::type` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGuiElement::elem_filters::mask` - union collapsed to object  
  _(CollisionLayerID|CollisionMask)_
* [runtime] `LuaGuiElement::elem_filters::mask` - union collapsed to object  
  _(CollisionLayerID|CollisionMask)_
* [runtime] `LuaGuiElement::elem_filters::name` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGuiElement::elem_filters::type` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGuiElement::elem_filters::type` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGuiElement::elem_filters::type` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGuiElement::elem_filters::name` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGuiElement::elem_filters::name` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGuiElement::elem_filters::type` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGuiElement::elem_filters::mask` - union collapsed to object  
  _(CollisionLayerID|CollisionMask)_
* [runtime] `LuaGuiElement::add::elem_filters` - union collapsed to object  
  _(ModSettingPrototypeFilter|SpaceLocationPrototypeFilter|DecorativePrototypeFilter|TilePrototypeFilter|AsteroidChunkPrototypeFilter|ItemPrototypeFilter|TechnologyPrototypeFilter|RecipePrototypeFilter|AchievementPrototypeFilter|VirtualSignalPrototypeFilter|EquipmentPrototypeFilter|FluidPrototypeFilter|EntityPrototypeFilter)_
* [runtime] `LuaItemCommon::get_blueprint_entities::return::wires` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint32,defines.wire_connector_id,uint32,defines.wire_connector_id]_
* [runtime] `LuaPlayer::mod_settings::value` - union collapsed to object  
  _(int32|double|boolean|string|Color)_
* [runtime] `LuaPlayer::stack_transfers::target` - union collapsed to object  
  _(LuaInventory|LuaEntity|LuaPlayer|LuaItem|LuaItemStack|LuaEquipment)_
* [runtime] `LuaSchedule::change_wait_condition::wait_condition::condition` - union collapsed to object  
  _(CircuitCondition|BlueprintItemIDAndQualityIDPair)_
* [runtime] `LuaSpacePlatform::destroy_asteroid_chunks::name` - union collapsed to object  
  _(AsteroidChunkID|array[AsteroidChunkID])_
* [runtime] `LuaSpacePlatform::find_asteroid_chunks_filtered::name` - union collapsed to object  
  _(AsteroidChunkID|array[AsteroidChunkID])_
* [runtime] `LuaSurface::clone_brush::destination_force` - union collapsed to object  
  _(LuaForce|string)_
* [runtime] `LuaSurface::count_entities_filtered::filter::name` - union collapsed to object  
  _(EntityID|array[EntityID])_
* [runtime] `LuaSurface::count_entities_filtered::filter::type` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaSurface::count_entities_filtered::filter::ghost_name` - union collapsed to object  
  _(EntityID|array[EntityID])_
* [runtime] `LuaSurface::count_entities_filtered::filter::ghost_type` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaSurface::count_entities_filtered::filter::direction` - union collapsed to object  
  _(defines.direction|array[defines.direction])_
* [runtime] `LuaSurface::count_entities_filtered::filter::collision_mask` - union collapsed to object  
  _(CollisionLayerID|array[CollisionLayerID]|dict[CollisionLayerID->literal(boolean:True)])_
* [runtime] `LuaSurface::count_tiles_filtered::filter::name` - union collapsed to object  
  _(TileID|array[TileID])_
* [runtime] `LuaSurface::count_tiles_filtered::filter::collision_mask` - union collapsed to object  
  _(CollisionLayerID|array[CollisionLayerID]|dict[CollisionLayerID->literal(boolean:True)])_
* [runtime] `LuaSurface::create_entity::stack` - union collapsed to object  
  _(string|ItemStackDefinition)_
* [runtime] `LuaSurface::create_entity::source` - union collapsed to object  
  _(LuaEntity|MapPosition)_
* [runtime] `LuaSurface::create_entity::cause` - union collapsed to object  
  _(LuaEntity|ForceID)_
* [runtime] `LuaSurface::destroy_decoratives::name` - union collapsed to object  
  _(DecorativeID|array[DecorativeID])_
* [runtime] `LuaSurface::destroy_decoratives::collision_mask` - union collapsed to object  
  _(CollisionLayerID|array[CollisionLayerID]|dict[CollisionLayerID->literal(boolean:True)])_
* [runtime] `LuaSurface::find_decoratives_filtered::name` - union collapsed to object  
  _(DecorativeID|array[DecorativeID])_
* [runtime] `LuaSurface::find_decoratives_filtered::collision_mask` - union collapsed to object  
  _(CollisionLayerID|array[CollisionLayerID]|dict[CollisionLayerID->literal(boolean:True)])_
* [runtime] `LuaTrainManager::get_train_stops::filter::type` - union collapsed to object  
  _(EntityID|array[EntityID])_
* [runtime] `LuaTrainManager::get_train_stops::filter::station_name` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaTrainManager::get_trains::filter::stock` - union collapsed to object  
  _(EntityID|array[EntityID])_
* [runtime] `LuaPlayer::set_controller::waypoints::target` - union collapsed to object  
  _(LuaEntity|LuaCommandable)_
* [runtime] `LuaEntityPrototype::attack_parameters::projectile_creation_parameters` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[RealOrientation,Vector]_
* [runtime] `LuaGameScript::get_vehicles::type` - union collapsed to object  
  _(EntityID|array[EntityID])_
* [runtime] `LuaGuiElement::elem_filters::type` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGuiElement::elem_filters::type` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGuiElement::elem_filters::mask` - union collapsed to object  
  _(CollisionLayerID|CollisionMask)_
* [runtime] `LuaGuiElement::elem_filters::mask` - union collapsed to object  
  _(CollisionLayerID|CollisionMask)_
* [runtime] `LuaGuiElement::elem_filters::name` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGuiElement::elem_filters::type` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGuiElement::elem_filters::type` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGuiElement::elem_filters::type` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGuiElement::elem_filters::name` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGuiElement::elem_filters::name` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGuiElement::elem_filters::type` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGuiElement::elem_filters::mask` - union collapsed to object  
  _(CollisionLayerID|CollisionMask)_
* [runtime] `LuaGuiElement::add::elem_filters` - union collapsed to object  
  _(ModSettingPrototypeFilter|SpaceLocationPrototypeFilter|DecorativePrototypeFilter|TilePrototypeFilter|AsteroidChunkPrototypeFilter|ItemPrototypeFilter|TechnologyPrototypeFilter|RecipePrototypeFilter|AchievementPrototypeFilter|VirtualSignalPrototypeFilter|EquipmentPrototypeFilter|FluidPrototypeFilter|EntityPrototypeFilter)_
* [runtime] `LuaItemCommon::get_blueprint_entities::return::wires` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint32,defines.wire_connector_id,uint32,defines.wire_connector_id]_
* [runtime] `LuaPlayer::mod_settings::value` - union collapsed to object  
  _(int32|double|boolean|string|Color)_
* [runtime] `LuaPlayer::stack_transfers::target` - union collapsed to object  
  _(LuaInventory|LuaEntity|LuaPlayer|LuaItem|LuaItemStack|LuaEquipment)_
* [runtime] `LuaSchedule::change_wait_condition::wait_condition::condition` - union collapsed to object  
  _(CircuitCondition|BlueprintItemIDAndQualityIDPair)_
* [runtime] `LuaSpacePlatform::destroy_asteroid_chunks::name` - union collapsed to object  
  _(AsteroidChunkID|array[AsteroidChunkID])_
* [runtime] `LuaSpacePlatform::find_asteroid_chunks_filtered::name` - union collapsed to object  
  _(AsteroidChunkID|array[AsteroidChunkID])_
* [runtime] `LuaSurface::clone_brush::destination_force` - union collapsed to object  
  _(LuaForce|string)_
* [runtime] `LuaSurface::count_entities_filtered::filter::name` - union collapsed to object  
  _(EntityID|array[EntityID])_
* [runtime] `LuaSurface::count_entities_filtered::filter::type` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaSurface::count_entities_filtered::filter::ghost_name` - union collapsed to object  
  _(EntityID|array[EntityID])_
* [runtime] `LuaSurface::count_entities_filtered::filter::ghost_type` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaSurface::count_entities_filtered::filter::direction` - union collapsed to object  
  _(defines.direction|array[defines.direction])_
* [runtime] `LuaSurface::count_entities_filtered::filter::collision_mask` - union collapsed to object  
  _(CollisionLayerID|array[CollisionLayerID]|dict[CollisionLayerID->literal(boolean:True)])_
* [runtime] `LuaSurface::count_tiles_filtered::filter::name` - union collapsed to object  
  _(TileID|array[TileID])_
* [runtime] `LuaSurface::count_tiles_filtered::filter::collision_mask` - union collapsed to object  
  _(CollisionLayerID|array[CollisionLayerID]|dict[CollisionLayerID->literal(boolean:True)])_
* [runtime] `LuaSurface::create_entity::stack` - union collapsed to object  
  _(string|ItemStackDefinition)_
* [runtime] `LuaSurface::create_entity::source` - union collapsed to object  
  _(LuaEntity|MapPosition)_
* [runtime] `LuaSurface::create_entity::cause` - union collapsed to object  
  _(LuaEntity|ForceID)_
* [runtime] `LuaSurface::destroy_decoratives::name` - union collapsed to object  
  _(DecorativeID|array[DecorativeID])_
* [runtime] `LuaSurface::destroy_decoratives::collision_mask` - union collapsed to object  
  _(CollisionLayerID|array[CollisionLayerID]|dict[CollisionLayerID->literal(boolean:True)])_
* [runtime] `LuaSurface::find_decoratives_filtered::name` - union collapsed to object  
  _(DecorativeID|array[DecorativeID])_
* [runtime] `LuaSurface::find_decoratives_filtered::collision_mask` - union collapsed to object  
  _(CollisionLayerID|array[CollisionLayerID]|dict[CollisionLayerID->literal(boolean:True)])_
* [runtime] `LuaTrainManager::get_train_stops::filter::type` - union collapsed to object  
  _(EntityID|array[EntityID])_
* [runtime] `LuaTrainManager::get_train_stops::filter::station_name` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaTrainManager::get_trains::filter::stock` - union collapsed to object  
  _(EntityID|array[EntityID])_
* [runtime] `LuaPlayer::set_controller::waypoints::target` - union collapsed to object  
  _(LuaEntity|LuaCommandable)_
* [runtime] `LuaEntityPrototype::attack_parameters::projectile_creation_parameters` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[RealOrientation,Vector]_
* [runtime] `LuaGameScript::get_vehicles::type` - union collapsed to object  
  _(EntityID|array[EntityID])_
* [runtime] `LuaGuiElement::elem_filters::type` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGuiElement::elem_filters::type` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGuiElement::elem_filters::mask` - union collapsed to object  
  _(CollisionLayerID|CollisionMask)_
* [runtime] `LuaGuiElement::elem_filters::mask` - union collapsed to object  
  _(CollisionLayerID|CollisionMask)_
* [runtime] `LuaGuiElement::elem_filters::name` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGuiElement::elem_filters::type` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGuiElement::elem_filters::type` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGuiElement::elem_filters::type` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGuiElement::elem_filters::name` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGuiElement::elem_filters::name` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGuiElement::elem_filters::type` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaGuiElement::elem_filters::mask` - union collapsed to object  
  _(CollisionLayerID|CollisionMask)_
* [runtime] `LuaGuiElement::add::elem_filters` - union collapsed to object  
  _(ModSettingPrototypeFilter|SpaceLocationPrototypeFilter|DecorativePrototypeFilter|TilePrototypeFilter|AsteroidChunkPrototypeFilter|ItemPrototypeFilter|TechnologyPrototypeFilter|RecipePrototypeFilter|AchievementPrototypeFilter|VirtualSignalPrototypeFilter|EquipmentPrototypeFilter|FluidPrototypeFilter|EntityPrototypeFilter)_
* [runtime] `LuaItemCommon::get_blueprint_entities::return::wires` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint32,defines.wire_connector_id,uint32,defines.wire_connector_id]_
* [runtime] `LuaPlayer::mod_settings::value` - union collapsed to object  
  _(int32|double|boolean|string|Color)_
* [runtime] `LuaPlayer::stack_transfers::target` - union collapsed to object  
  _(LuaInventory|LuaEntity|LuaPlayer|LuaItem|LuaItemStack|LuaEquipment)_
* [runtime] `LuaSchedule::change_wait_condition::wait_condition::condition` - union collapsed to object  
  _(CircuitCondition|BlueprintItemIDAndQualityIDPair)_
* [runtime] `LuaSpacePlatform::destroy_asteroid_chunks::name` - union collapsed to object  
  _(AsteroidChunkID|array[AsteroidChunkID])_
* [runtime] `LuaSpacePlatform::find_asteroid_chunks_filtered::name` - union collapsed to object  
  _(AsteroidChunkID|array[AsteroidChunkID])_
* [runtime] `LuaSurface::clone_brush::destination_force` - union collapsed to object  
  _(LuaForce|string)_
* [runtime] `LuaSurface::count_entities_filtered::filter::name` - union collapsed to object  
  _(EntityID|array[EntityID])_
* [runtime] `LuaSurface::count_entities_filtered::filter::type` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaSurface::count_entities_filtered::filter::ghost_name` - union collapsed to object  
  _(EntityID|array[EntityID])_
* [runtime] `LuaSurface::count_entities_filtered::filter::ghost_type` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaSurface::count_entities_filtered::filter::direction` - union collapsed to object  
  _(defines.direction|array[defines.direction])_
* [runtime] `LuaSurface::count_entities_filtered::filter::collision_mask` - union collapsed to object  
  _(CollisionLayerID|array[CollisionLayerID]|dict[CollisionLayerID->literal(boolean:True)])_
* [runtime] `LuaSurface::count_tiles_filtered::filter::name` - union collapsed to object  
  _(TileID|array[TileID])_
* [runtime] `LuaSurface::count_tiles_filtered::filter::collision_mask` - union collapsed to object  
  _(CollisionLayerID|array[CollisionLayerID]|dict[CollisionLayerID->literal(boolean:True)])_
* [runtime] `LuaSurface::create_entity::stack` - union collapsed to object  
  _(string|ItemStackDefinition)_
* [runtime] `LuaSurface::create_entity::source` - union collapsed to object  
  _(LuaEntity|MapPosition)_
* [runtime] `LuaSurface::create_entity::cause` - union collapsed to object  
  _(LuaEntity|ForceID)_
* [runtime] `LuaSurface::destroy_decoratives::name` - union collapsed to object  
  _(DecorativeID|array[DecorativeID])_
* [runtime] `LuaSurface::destroy_decoratives::collision_mask` - union collapsed to object  
  _(CollisionLayerID|array[CollisionLayerID]|dict[CollisionLayerID->literal(boolean:True)])_
* [runtime] `LuaSurface::find_decoratives_filtered::name` - union collapsed to object  
  _(DecorativeID|array[DecorativeID])_
* [runtime] `LuaSurface::find_decoratives_filtered::collision_mask` - union collapsed to object  
  _(CollisionLayerID|array[CollisionLayerID]|dict[CollisionLayerID->literal(boolean:True)])_
* [runtime] `LuaTrainManager::get_train_stops::filter::type` - union collapsed to object  
  _(EntityID|array[EntityID])_
* [runtime] `LuaTrainManager::get_train_stops::filter::station_name` - union collapsed to object  
  _(string|array[string])_
* [runtime] `LuaTrainManager::get_trains::filter::stock` - union collapsed to object  
  _(EntityID|array[EntityID])_
* [runtime] `LuaPlayer::set_controller::waypoints::target` - union collapsed to object  
  _(LuaEntity|LuaCommandable)_
* [runtime] `AmmoItemPrototype::ammo_type` - union collapsed to object  
  _(AmmoType|array[AmmoType])_
* [runtime] `AnimationPrototype::position` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[SpriteSizeType,SpriteSizeType]_
* [runtime] `AnimationPrototype::size` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[SpriteSizeType,SpriteSizeType]_
* [runtime] `AnimationPrototype::size` - union collapsed to object  
  _(SpriteSizeType|[SpriteSizeType,SpriteSizeType])_
* [runtime] `ArtilleryFlarePrototype::ended_in_water_trigger_effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `ArtilleryFlarePrototype::ended_in_water_trigger_effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `ArtilleryFlarePrototype::regular_trigger_effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `ArtilleryFlarePrototype::regular_trigger_effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `ArtilleryProjectilePrototype::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ArtilleryProjectilePrototype::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ArtilleryProjectilePrototype::final_action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ArtilleryProjectilePrototype::final_action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `AssemblingMachinePrototype::circuit_connector` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[CircuitConnectorDefinition,CircuitConnectorDefinition,CircuitConnectorDefinition,CircuitConnectorDefinition]_
* [runtime] `AssemblingMachinePrototype::circuit_connector_flipped` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[CircuitConnectorDefinition,CircuitConnectorDefinition,CircuitConnectorDefinition,CircuitConnectorDefinition]_
* [runtime] `AsteroidChunkPrototype::dying_trigger_effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `AsteroidChunkPrototype::dying_trigger_effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `AsteroidCollectorPrototype::circuit_connector` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[CircuitConnectorDefinition,CircuitConnectorDefinition,CircuitConnectorDefinition,CircuitConnectorDefinition]_
* [runtime] `AsteroidCollectorPrototype::energy_source` - union collapsed to object  
  _(ElectricEnergySource|VoidEnergySource)_
* [runtime] `BeaconPrototype::energy_source` - union collapsed to object  
  _(ElectricEnergySource|VoidEnergySource)_
* [runtime] `BeamPrototype::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `BeamPrototype::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `BoilerPrototype::circuit_connector` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[CircuitConnectorDefinition,CircuitConnectorDefinition,CircuitConnectorDefinition,CircuitConnectorDefinition]_
* [runtime] `CaptureRobotPrototype::destroy_action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `CaptureRobotPrototype::destroy_action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `CarPrototype::energy_source` - union collapsed to object  
  _(BurnerEnergySource|VoidEnergySource)_
* [runtime] `CargoPodPrototype::impact_trigger` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `CargoPodPrototype::impact_trigger` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ChainActiveTriggerPrototype::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ChainActiveTriggerPrototype::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `CharacterPrototype::tool_attack_result` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `CharacterPrototype::tool_attack_result` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `CombatRobotPrototype::destroy_action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `CombatRobotPrototype::destroy_action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `CombinatorPrototype::activity_led_light_offsets` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[Vector,Vector,Vector,Vector]_
* [runtime] `CombinatorPrototype::energy_source` - union collapsed to object  
  _(ElectricEnergySource|VoidEnergySource)_
* [runtime] `CombinatorPrototype::input_connection_points` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[WireConnectionPoint,WireConnectionPoint,WireConnectionPoint,WireConnectionPoint]_
* [runtime] `CombinatorPrototype::output_connection_points` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[WireConnectionPoint,WireConnectionPoint,WireConnectionPoint,WireConnectionPoint]_
* [runtime] `CombinatorPrototype::screen_light_offsets` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[Vector,Vector,Vector,Vector]_
* [runtime] `ConstantCombinatorPrototype::activity_led_light_offsets` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[Vector,Vector,Vector,Vector]_
* [runtime] `ConstantCombinatorPrototype::circuit_wire_connection_points` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[WireConnectionPoint,WireConnectionPoint,WireConnectionPoint,WireConnectionPoint]_
* [runtime] `DecorativePrototype::placed_effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `DecorativePrototype::placed_effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `DecorativePrototype::stateless_visualisation` - union collapsed to object  
  _(StatelessVisualisation|array[StatelessVisualisation])_
* [runtime] `DecorativePrototype::stateless_visualisation_variations` - union collapsed to object  
  _(StatelessVisualisation|array[StatelessVisualisation])_
* [runtime] `DecorativePrototype::trigger_effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `DecorativePrototype::trigger_effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `DelayedActiveTriggerPrototype::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `DelayedActiveTriggerPrototype::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `DeliverImpactCombination::trigger_effect_item` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `DeliverImpactCombination::trigger_effect_item` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `DisplayPanelPrototype::circuit_connector` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[CircuitConnectorDefinition,CircuitConnectorDefinition,CircuitConnectorDefinition,CircuitConnectorDefinition]_
* [runtime] `DontBuildEntityAchievementPrototype::dont_build` - union collapsed to object  
  _(EntityID|array[EntityID])_
* [runtime] `DontBuildEntityAchievementPrototype::research_with` - union collapsed to object  
  _(ItemID|array[ItemID])_
* [runtime] `DontResearchBeforeResearchingAchievementPrototype::dont_research` - union collapsed to object  
  _(ItemID|array[ItemID])_
* [runtime] `DontResearchBeforeResearchingAchievementPrototype::research_with` - union collapsed to object  
  _(ItemID|array[ItemID])_
* [runtime] `DontUseEntityInEnergyProductionAchievementPrototype::excluded` - union collapsed to object  
  _(EntityID|array[EntityID])_
* [runtime] `DontUseEntityInEnergyProductionAchievementPrototype::included` - union collapsed to object  
  _(EntityID|array[EntityID])_
* [runtime] `ElectricTurretPrototype::energy_source` - union collapsed to object  
  _(ElectricEnergySource|VoidEnergySource)_
* [runtime] `EnemySpawnerPrototype::spawn_blocked_trigger` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `EnemySpawnerPrototype::spawn_blocked_trigger` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `EnemySpawnerPrototype::spawning_cooldown` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[double,double]_
* [runtime] `EntityPrototype::ambient_sounds` - union collapsed to object  
  _(WorldAmbientSoundDefinition|array[WorldAmbientSoundDefinition])_
* [runtime] `EntityPrototype::created_effect` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `EntityPrototype::created_effect` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `EntityPrototype::placeable_by` - union collapsed to object  
  _(ItemToPlace|array[ItemToPlace])_
* [runtime] `EntityPrototype::remains_when_mined` - union collapsed to object  
  _(EntityID|array[EntityID])_
* [runtime] `EntityPrototype::stateless_visualisation` - union collapsed to object  
  _(StatelessVisualisation|array[StatelessVisualisation])_
* [runtime] `EntityWithHealthPrototype::attack_reaction` - union collapsed to object  
  _(AttackReactionItem|array[AttackReactionItem])_
* [runtime] `EntityWithHealthPrototype::corpse` - union collapsed to object  
  _(EntityID|array[EntityID])_
* [runtime] `EntityWithHealthPrototype::damaged_trigger_effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `EntityWithHealthPrototype::damaged_trigger_effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `EntityWithHealthPrototype::dying_explosion` - union collapsed to object  
  _(ExplosionDefinition|array[ExplosionDefinition])_
* [runtime] `EntityWithHealthPrototype::dying_trigger_effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `EntityWithHealthPrototype::dying_trigger_effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `ExplosionPrototype::explosion_effect` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ExplosionPrototype::explosion_effect` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `FireFlamePrototype::on_damage_tick_effect` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `FireFlamePrototype::on_damage_tick_effect` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `FireFlamePrototype::on_fuel_added_action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `FireFlamePrototype::on_fuel_added_action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `FluidStreamPrototype::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `FluidStreamPrototype::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `FluidStreamPrototype::initial_action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `FluidStreamPrototype::initial_action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `FluidWagonPrototype::connection_category` - union collapsed to object  
  _(string|array[string])_
* [runtime] `FurnacePrototype::circuit_connector` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[CircuitConnectorDefinition,CircuitConnectorDefinition,CircuitConnectorDefinition,CircuitConnectorDefinition]_
* [runtime] `FurnacePrototype::circuit_connector_flipped` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[CircuitConnectorDefinition,CircuitConnectorDefinition,CircuitConnectorDefinition,CircuitConnectorDefinition]_
* [runtime] `InserterPrototype::circuit_connector` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[CircuitConnectorDefinition,CircuitConnectorDefinition,CircuitConnectorDefinition,CircuitConnectorDefinition]_
* [runtime] `InserterPrototype::circuit_connector_flipped` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[CircuitConnectorDefinition,CircuitConnectorDefinition,CircuitConnectorDefinition,CircuitConnectorDefinition]_
* [runtime] `ItemPrototype::destroyed_by_dropping_trigger` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ItemPrototype::destroyed_by_dropping_trigger` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `KillAchievementPrototype::damage_dealer` - union collapsed to object  
  _(EntityID|array[EntityID])_
* [runtime] `KillAchievementPrototype::to_kill` - union collapsed to object  
  _(EntityID|array[EntityID])_
* [runtime] `LampPrototype::energy_source` - union collapsed to object  
  _(ElectricEnergySource|VoidEnergySource)_
* [runtime] `LandMinePrototype::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `LandMinePrototype::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `LightningPrototype::attractor_hit_effect` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `LightningPrototype::attractor_hit_effect` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `LightningPrototype::strike_effect` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `LightningPrototype::strike_effect` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `LoaderPrototype::energy_source` - union collapsed to object  
  _(ElectricEnergySource|HeatEnergySource|FluidEnergySource|VoidEnergySource)_
* [runtime] `LocomotivePrototype::energy_source` - union collapsed to object  
  _(BurnerEnergySource|VoidEnergySource)_
* [runtime] `MiningDrillPrototype::circuit_connector` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[CircuitConnectorDefinition,CircuitConnectorDefinition,CircuitConnectorDefinition,CircuitConnectorDefinition]_
* [runtime] `MiningDrillPrototype::circuit_connector_flipped` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[CircuitConnectorDefinition,CircuitConnectorDefinition,CircuitConnectorDefinition,CircuitConnectorDefinition]_
* [runtime] `ModuleTransferAchievementPrototype::module` - union collapsed to object  
  _(ItemID|array[ItemID])_
* [runtime] `NightVisionEquipmentPrototype::color_lookup` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[double,ColorLookupTable]_
* [runtime] `OffshorePumpPrototype::circuit_connector` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[CircuitConnectorDefinition,CircuitConnectorDefinition,CircuitConnectorDefinition,CircuitConnectorDefinition]_
* [runtime] `ParticlePrototype::ended_in_water_trigger_effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `ParticlePrototype::ended_in_water_trigger_effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `ParticlePrototype::ended_on_ground_trigger_effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `ParticlePrototype::ended_on_ground_trigger_effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `ParticlePrototype::regular_trigger_effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `ParticlePrototype::regular_trigger_effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `PipePrototype::circuit_connector` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[CircuitConnectorDefinition,CircuitConnectorDefinition,CircuitConnectorDefinition,CircuitConnectorDefinition]_
* [runtime] `PipeToGroundPrototype::circuit_connector` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[CircuitConnectorDefinition,CircuitConnectorDefinition,CircuitConnectorDefinition,CircuitConnectorDefinition]_
* [runtime] `PlanetPrototype::player_effects` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `PlanetPrototype::player_effects` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ProcessionPrototype::procession_style` - union collapsed to object  
  _(uint32|array[uint32])_
* [runtime] `ProgrammableSpeakerPrototype::energy_source` - union collapsed to object  
  _(ElectricEnergySource|VoidEnergySource)_
* [runtime] `ProjectilePrototype::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ProjectilePrototype::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ProjectilePrototype::final_action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ProjectilePrototype::final_action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `PumpPrototype::circuit_connector` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[CircuitConnectorDefinition,CircuitConnectorDefinition,CircuitConnectorDefinition,CircuitConnectorDefinition]_
* [runtime] `ReactorPrototype::meltdown_action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ReactorPrototype::meltdown_action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `RoboportPrototype::close_door_trigger_effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `RoboportPrototype::close_door_trigger_effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `RoboportPrototype::energy_source` - union collapsed to object  
  _(ElectricEnergySource|VoidEnergySource)_
* [runtime] `RoboportPrototype::open_door_trigger_effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `RoboportPrototype::open_door_trigger_effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `RobotWithLogisticInterfacePrototype::destroy_action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `RobotWithLogisticInterfacePrototype::destroy_action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `RocketSiloPrototype::alarm_trigger` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `RocketSiloPrototype::alarm_trigger` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `RocketSiloPrototype::clamps_off_trigger` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `RocketSiloPrototype::clamps_off_trigger` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `RocketSiloPrototype::clamps_on_trigger` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `RocketSiloPrototype::clamps_on_trigger` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `RocketSiloPrototype::doors_trigger` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `RocketSiloPrototype::doors_trigger` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `RocketSiloPrototype::raise_rocket_trigger` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `RocketSiloPrototype::raise_rocket_trigger` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `RocketSiloRocketPrototype::flying_trigger` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `RocketSiloRocketPrototype::flying_trigger` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `RollingStockPrototype::drive_over_elevated_tie_trigger` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `RollingStockPrototype::drive_over_elevated_tie_trigger` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `RollingStockPrototype::drive_over_tie_trigger` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `RollingStockPrototype::drive_over_tie_trigger` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `SimpleEntityPrototype::stateless_visualisation_variations` - union collapsed to object  
  _(StatelessVisualisation|array[StatelessVisualisation])_
* [runtime] `SimpleEntityWithOwnerPrototype::stateless_visualisation_variations` - union collapsed to object  
  _(StatelessVisualisation|array[StatelessVisualisation])_
* [runtime] `SmokeWithTriggerPrototype::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `SmokeWithTriggerPrototype::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `SoundPrototype::modifiers` - union collapsed to object  
  _(SoundModifier|array[SoundModifier])_
* [runtime] `SoundPrototype::variations` - union collapsed to object  
  _(SoundDefinition|array[SoundDefinition])_
* [runtime] `SpacePlatformStarterPackPrototype::trigger` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `SpacePlatformStarterPackPrototype::trigger` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `SpiderVehiclePrototype::energy_source` - union collapsed to object  
  _(BurnerEnergySource|VoidEnergySource)_
* [runtime] `SplitterPrototype::circuit_connector` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[CircuitConnectorDefinition,CircuitConnectorDefinition,CircuitConnectorDefinition,CircuitConnectorDefinition]_
* [runtime] `SpritePrototype::position` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[SpriteSizeType,SpriteSizeType]_
* [runtime] `SpritePrototype::size` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[SpriteSizeType,SpriteSizeType]_
* [runtime] `SpritePrototype::size` - union collapsed to object  
  _(SpriteSizeType|[SpriteSizeType,SpriteSizeType])_
* [runtime] `StorageTankPrototype::circuit_connector` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[CircuitConnectorDefinition,CircuitConnectorDefinition,CircuitConnectorDefinition,CircuitConnectorDefinition]_
* [runtime] `TilePrototype::ambient_sounds` - union collapsed to object  
  _(WorldAmbientSoundDefinition|array[WorldAmbientSoundDefinition])_
* [runtime] `TilePrototype::bound_decoratives` - union collapsed to object  
  _(DecorativeID|array[DecorativeID])_
* [runtime] `TilePrototype::build_sound` - union collapsed to object  
  _(Sound|TileBuildSound)_
* [runtime] `TilePrototype::default_destroyed_dropped_item_trigger` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `TilePrototype::default_destroyed_dropped_item_trigger` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `TilePrototype::dying_explosion` - union collapsed to object  
  _(ExplosionDefinition|array[ExplosionDefinition])_
* [runtime] `TilePrototype::placeable_by` - union collapsed to object  
  _(ItemToPlace|array[ItemToPlace])_
* [runtime] `TilePrototype::trigger_effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `TilePrototype::trigger_effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `TrainStopPrototype::circuit_connector` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[CircuitConnectorDefinition,CircuitConnectorDefinition,CircuitConnectorDefinition,CircuitConnectorDefinition]_
* [runtime] `TreePrototype::stateless_visualisation_variations` - union collapsed to object  
  _(StatelessVisualisation|array[StatelessVisualisation])_
* [runtime] `TurretPrototype::folded_state_corpse` - union collapsed to object  
  _(EntityID|array[EntityID])_
* [runtime] `UtilityConstants::daytime_color_lookup` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[double,ColorLookupTable]_
* [runtime] `VehiclePrototype::crash_trigger` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `VehiclePrototype::crash_trigger` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `VehiclePrototype::stop_trigger` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `VehiclePrototype::stop_trigger` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `ActiveDefenseEquipmentPrototype::attack_parameters::projectile_creation_parameters` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[RealOrientation,Vector]_
* [runtime] `ActiveDefenseEquipmentPrototype::attack_parameters::projectile_creation_parameters` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[RealOrientation,Vector]_
* [runtime] `ActiveDefenseEquipmentPrototype::attack_parameters::gun_center_shift` - union collapsed to object  
  _(Vector|GunShift4Way)_
* [runtime] `AgriculturalTowerPrototype::harvesting_sound::variations` - union collapsed to object  
  _(SoundDefinition|array[SoundDefinition])_
* [runtime] `AgriculturalTowerPrototype::harvesting_sound::modifiers` - union collapsed to object  
  _(SoundModifier|array[SoundModifier])_
* [runtime] `AmmoItemPrototype::ammo_type::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `AmmoItemPrototype::ammo_type::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ArtilleryFlarePrototype::ended_in_water_trigger_effect::non_colliding_fail_result` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ArtilleryFlarePrototype::ended_in_water_trigger_effect::non_colliding_fail_result` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ArtilleryFlarePrototype::ended_in_water_trigger_effect::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ArtilleryFlarePrototype::ended_in_water_trigger_effect::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ArtilleryProjectilePrototype::action::range_effects` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `ArtilleryProjectilePrototype::action::range_effects` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `AsteroidChunkPrototype::graphics_set::variations` - union collapsed to object  
  _(AsteroidVariation|array[AsteroidVariation])_
* [runtime] `AsteroidChunkPrototype::graphics_set::lights` - union collapsed to object  
  _(LightProperties|array[LightProperties])_
* [runtime] `AsteroidChunkPrototype::minable::mining_trigger` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `AsteroidChunkPrototype::minable::mining_trigger` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `DecorativePrototype::autoplace::tile_restriction` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[TileID,TileID]_
* [runtime] `DecorativePrototype::stateless_visualisation::offset_x` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `DecorativePrototype::stateless_visualisation::offset_y` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `DecorativePrototype::stateless_visualisation::offset_z` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `DecorativePrototype::stateless_visualisation::speed_x` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `DecorativePrototype::stateless_visualisation::speed_y` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `DecorativePrototype::stateless_visualisation::speed_z` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `DecorativePrototype::stateless_visualisation::scale` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `DecorativePrototype::stateless_visualisation::nested_visualisations` - union collapsed to object  
  _(StatelessVisualisation|array[StatelessVisualisation])_
* [runtime] `EntityPrototype::working_sound::main_sounds` - union collapsed to object  
  _(MainSound|array[MainSound])_
* [runtime] `EntityPrototype::working_sound::sound_accents` - union collapsed to object  
  _(SoundAccent|array[SoundAccent])_
* [runtime] `EntityWithHealthPrototype::attack_reaction::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `EntityWithHealthPrototype::attack_reaction::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ItemPrototype::spoil_to_trigger_result::trigger` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ItemPrototype::spoil_to_trigger_result::trigger` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `PlanetPrototype::persistent_ambient_sounds::base_ambience` - union collapsed to object  
  _(PersistentWorldAmbientSoundDefinition|array[PersistentWorldAmbientSoundDefinition])_
* [runtime] `PlanetPrototype::persistent_ambient_sounds::wind` - union collapsed to object  
  _(PersistentWorldAmbientSoundDefinition|array[PersistentWorldAmbientSoundDefinition])_
* [runtime] `PlanetPrototype::persistent_ambient_sounds::semi_persistent` - union collapsed to object  
  _(SemiPersistentWorldAmbientSoundDefinition|array[SemiPersistentWorldAmbientSoundDefinition])_
* [runtime] `PlanetPrototype::surface_render_parameters::day_night_cycle_color_lookup` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[double,ColorLookupTable]_
* [runtime] `SegmentPrototype::update_effects::effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `SegmentPrototype::update_effects::effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `SoundPrototype::variations::modifiers` - union collapsed to object  
  _(SoundModifier|array[SoundModifier])_
* [runtime] `SpiderLegPrototype::lower_leg_dying_trigger_effects::effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `SpiderLegPrototype::lower_leg_dying_trigger_effects::effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `SpiderUnitPrototype::spider_engine::legs` - union collapsed to object  
  _(SpiderLegSpecification|array[SpiderLegSpecification])_
* [runtime] `SplitterPrototype::default_input_left_condition::second` - union collapsed to object  
  _(SignalIDConnector|int32)_
* [runtime] `TechnologyPrototype::unit::ingredients` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[ItemID,uint16]_
* [runtime] `ThrusterPrototype::plumes::stateless_visualisations` - union collapsed to object  
  _(PlumeEffect|array[PlumeEffect])_
* [runtime] `TileEffectDefinition::water::animation_scale` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `TileEffectDefinition::water::animation_scale` - union collapsed to object  
  _(float|[float,float])_
* [runtime] `TileEffectDefinition::water::dark_threshold` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `TileEffectDefinition::water::dark_threshold` - union collapsed to object  
  _(float|[float,float])_
* [runtime] `TileEffectDefinition::water::reflection_threshold` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `TileEffectDefinition::water::reflection_threshold` - union collapsed to object  
  _(float|[float,float])_
* [runtime] `TileEffectDefinition::water::specular_threshold` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `TileEffectDefinition::water::specular_threshold` - union collapsed to object  
  _(float|[float,float])_
* [runtime] `TurretPrototype::graphics_set::base_visualisation` - union collapsed to object  
  _(TurretBaseVisualisation|array[TurretBaseVisualisation])_
* [runtime] `ActiveDefenseEquipmentPrototype::attack_parameters::projectile_creation_parameters` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[RealOrientation,Vector]_
* [runtime] `ActiveDefenseEquipmentPrototype::attack_parameters::projectile_creation_parameters` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[RealOrientation,Vector]_
* [runtime] `ActiveDefenseEquipmentPrototype::attack_parameters::gun_center_shift` - union collapsed to object  
  _(Vector|GunShift4Way)_
* [runtime] `AgriculturalTowerPrototype::harvesting_sound::variations` - union collapsed to object  
  _(SoundDefinition|array[SoundDefinition])_
* [runtime] `AgriculturalTowerPrototype::harvesting_sound::modifiers` - union collapsed to object  
  _(SoundModifier|array[SoundModifier])_
* [runtime] `AmmoItemPrototype::ammo_type::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `AmmoItemPrototype::ammo_type::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ArtilleryFlarePrototype::ended_in_water_trigger_effect::non_colliding_fail_result` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ArtilleryFlarePrototype::ended_in_water_trigger_effect::non_colliding_fail_result` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ArtilleryFlarePrototype::ended_in_water_trigger_effect::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ArtilleryFlarePrototype::ended_in_water_trigger_effect::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ArtilleryProjectilePrototype::action::range_effects` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `ArtilleryProjectilePrototype::action::range_effects` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `AsteroidChunkPrototype::graphics_set::variations` - union collapsed to object  
  _(AsteroidVariation|array[AsteroidVariation])_
* [runtime] `AsteroidChunkPrototype::graphics_set::lights` - union collapsed to object  
  _(LightProperties|array[LightProperties])_
* [runtime] `AsteroidChunkPrototype::minable::mining_trigger` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `AsteroidChunkPrototype::minable::mining_trigger` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `DecorativePrototype::autoplace::tile_restriction` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[TileID,TileID]_
* [runtime] `DecorativePrototype::stateless_visualisation::offset_x` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `DecorativePrototype::stateless_visualisation::offset_y` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `DecorativePrototype::stateless_visualisation::offset_z` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `DecorativePrototype::stateless_visualisation::speed_x` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `DecorativePrototype::stateless_visualisation::speed_y` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `DecorativePrototype::stateless_visualisation::speed_z` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `DecorativePrototype::stateless_visualisation::scale` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `DecorativePrototype::stateless_visualisation::nested_visualisations` - union collapsed to object  
  _(StatelessVisualisation|array[StatelessVisualisation])_
* [runtime] `EntityPrototype::working_sound::main_sounds` - union collapsed to object  
  _(MainSound|array[MainSound])_
* [runtime] `EntityPrototype::working_sound::sound_accents` - union collapsed to object  
  _(SoundAccent|array[SoundAccent])_
* [runtime] `EntityWithHealthPrototype::attack_reaction::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `EntityWithHealthPrototype::attack_reaction::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ItemPrototype::spoil_to_trigger_result::trigger` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ItemPrototype::spoil_to_trigger_result::trigger` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `PlanetPrototype::persistent_ambient_sounds::base_ambience` - union collapsed to object  
  _(PersistentWorldAmbientSoundDefinition|array[PersistentWorldAmbientSoundDefinition])_
* [runtime] `PlanetPrototype::persistent_ambient_sounds::wind` - union collapsed to object  
  _(PersistentWorldAmbientSoundDefinition|array[PersistentWorldAmbientSoundDefinition])_
* [runtime] `PlanetPrototype::persistent_ambient_sounds::semi_persistent` - union collapsed to object  
  _(SemiPersistentWorldAmbientSoundDefinition|array[SemiPersistentWorldAmbientSoundDefinition])_
* [runtime] `PlanetPrototype::surface_render_parameters::day_night_cycle_color_lookup` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[double,ColorLookupTable]_
* [runtime] `SegmentPrototype::update_effects::effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `SegmentPrototype::update_effects::effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `SoundPrototype::variations::modifiers` - union collapsed to object  
  _(SoundModifier|array[SoundModifier])_
* [runtime] `SpiderLegPrototype::lower_leg_dying_trigger_effects::effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `SpiderLegPrototype::lower_leg_dying_trigger_effects::effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `SpiderUnitPrototype::spider_engine::legs` - union collapsed to object  
  _(SpiderLegSpecification|array[SpiderLegSpecification])_
* [runtime] `SplitterPrototype::default_input_left_condition::second` - union collapsed to object  
  _(SignalIDConnector|int32)_
* [runtime] `TechnologyPrototype::unit::ingredients` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[ItemID,uint16]_
* [runtime] `ThrusterPrototype::plumes::stateless_visualisations` - union collapsed to object  
  _(PlumeEffect|array[PlumeEffect])_
* [runtime] `TileEffectDefinition::water::animation_scale` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `TileEffectDefinition::water::animation_scale` - union collapsed to object  
  _(float|[float,float])_
* [runtime] `TileEffectDefinition::water::dark_threshold` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `TileEffectDefinition::water::dark_threshold` - union collapsed to object  
  _(float|[float,float])_
* [runtime] `TileEffectDefinition::water::reflection_threshold` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `TileEffectDefinition::water::reflection_threshold` - union collapsed to object  
  _(float|[float,float])_
* [runtime] `TileEffectDefinition::water::specular_threshold` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `TileEffectDefinition::water::specular_threshold` - union collapsed to object  
  _(float|[float,float])_
* [runtime] `TurretPrototype::graphics_set::base_visualisation` - union collapsed to object  
  _(TurretBaseVisualisation|array[TurretBaseVisualisation])_
* [runtime] `AmbientSound::variable_sound::layers::sublayer_starting_offset` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `AmbientSound::variable_sound::layers::sublayer_starting_offset` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `AmbientSound::variable_sound::layers::sublayer_starting_offset` - union collapsed to object  
  _(RandomRange|ProbabilityTable)_
* [runtime] `AmbientSound::variable_sound::layers::sublayer_offset` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `AmbientSound::variable_sound::layers::sublayer_offset` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `AmbientSound::variable_sound::layers::sublayer_offset` - union collapsed to object  
  _(RandomRange|ProbabilityTable)_
* [runtime] `AmbientSound::variable_sound::layers::sample_length` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `AmbientSound::variable_sound::states::start_pause` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `AmbientSound::variable_sound::states::end_pause` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `AmbientSound::variable_sound::states::number_of_enabled_layers` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `BoilerPrototype::fluid_box::pipe_connections::positions` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[MapPosition,MapPosition,MapPosition,MapPosition]_
* [runtime] `BoilerPrototype::fluid_box::pipe_connections::connection_category` - union collapsed to object  
  _(string|array[string])_
* [runtime] `CargoBayPrototype::graphics_set::connections::tileset_mapping` - union collapsed to object  
  _(uint8|array[uint8])_
* [runtime] `PlanetPrototype::persistent_ambient_sounds::crossfade::order` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[(literal(string:wind)|literal(string:base_ambience)),(literal(string:wind)|literal(string:base_ambience))]_
* [runtime] `RailPrototype::fence_pictures::side_A::ends` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[RailFenceDirectionSet,RailFenceDirectionSet,RailFenceDirectionSet,RailFenceDirectionSet]_
* [runtime] `RailPrototype::fence_pictures::side_A::ends_upper` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[RailFenceDirectionSet,RailFenceDirectionSet,RailFenceDirectionSet,RailFenceDirectionSet]_
* [runtime] `SpiderUnitPrototype::spider_engine::legs::leg_hit_the_ground_trigger` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `SpiderUnitPrototype::spider_engine::legs::leg_hit_the_ground_trigger` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `SpiderUnitPrototype::spider_engine::legs::leg_hit_the_ground_when_attacking_trigger` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `SpiderUnitPrototype::spider_engine::legs::leg_hit_the_ground_when_attacking_trigger` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `ActiveDefenseEquipmentPrototype::attack_parameters::projectile_creation_parameters` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[RealOrientation,Vector]_
* [runtime] `ActiveDefenseEquipmentPrototype::attack_parameters::projectile_creation_parameters` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[RealOrientation,Vector]_
* [runtime] `ActiveDefenseEquipmentPrototype::attack_parameters::gun_center_shift` - union collapsed to object  
  _(Vector|GunShift4Way)_
* [runtime] `AgriculturalTowerPrototype::harvesting_sound::variations` - union collapsed to object  
  _(SoundDefinition|array[SoundDefinition])_
* [runtime] `AgriculturalTowerPrototype::harvesting_sound::modifiers` - union collapsed to object  
  _(SoundModifier|array[SoundModifier])_
* [runtime] `AmmoItemPrototype::ammo_type::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `AmmoItemPrototype::ammo_type::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ArtilleryFlarePrototype::ended_in_water_trigger_effect::non_colliding_fail_result` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ArtilleryFlarePrototype::ended_in_water_trigger_effect::non_colliding_fail_result` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ArtilleryFlarePrototype::ended_in_water_trigger_effect::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ArtilleryFlarePrototype::ended_in_water_trigger_effect::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ArtilleryProjectilePrototype::action::range_effects` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `ArtilleryProjectilePrototype::action::range_effects` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `AsteroidChunkPrototype::graphics_set::variations` - union collapsed to object  
  _(AsteroidVariation|array[AsteroidVariation])_
* [runtime] `AsteroidChunkPrototype::graphics_set::lights` - union collapsed to object  
  _(LightProperties|array[LightProperties])_
* [runtime] `AsteroidChunkPrototype::minable::mining_trigger` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `AsteroidChunkPrototype::minable::mining_trigger` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `DecorativePrototype::autoplace::tile_restriction` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[TileID,TileID]_
* [runtime] `DecorativePrototype::stateless_visualisation::offset_x` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `DecorativePrototype::stateless_visualisation::offset_y` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `DecorativePrototype::stateless_visualisation::offset_z` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `DecorativePrototype::stateless_visualisation::speed_x` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `DecorativePrototype::stateless_visualisation::speed_y` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `DecorativePrototype::stateless_visualisation::speed_z` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `DecorativePrototype::stateless_visualisation::scale` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `DecorativePrototype::stateless_visualisation::nested_visualisations` - union collapsed to object  
  _(StatelessVisualisation|array[StatelessVisualisation])_
* [runtime] `EntityPrototype::working_sound::main_sounds` - union collapsed to object  
  _(MainSound|array[MainSound])_
* [runtime] `EntityPrototype::working_sound::sound_accents` - union collapsed to object  
  _(SoundAccent|array[SoundAccent])_
* [runtime] `EntityWithHealthPrototype::attack_reaction::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `EntityWithHealthPrototype::attack_reaction::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ItemPrototype::spoil_to_trigger_result::trigger` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ItemPrototype::spoil_to_trigger_result::trigger` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `PlanetPrototype::persistent_ambient_sounds::base_ambience` - union collapsed to object  
  _(PersistentWorldAmbientSoundDefinition|array[PersistentWorldAmbientSoundDefinition])_
* [runtime] `PlanetPrototype::persistent_ambient_sounds::wind` - union collapsed to object  
  _(PersistentWorldAmbientSoundDefinition|array[PersistentWorldAmbientSoundDefinition])_
* [runtime] `PlanetPrototype::persistent_ambient_sounds::semi_persistent` - union collapsed to object  
  _(SemiPersistentWorldAmbientSoundDefinition|array[SemiPersistentWorldAmbientSoundDefinition])_
* [runtime] `PlanetPrototype::surface_render_parameters::day_night_cycle_color_lookup` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[double,ColorLookupTable]_
* [runtime] `SegmentPrototype::update_effects::effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `SegmentPrototype::update_effects::effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `SoundPrototype::variations::modifiers` - union collapsed to object  
  _(SoundModifier|array[SoundModifier])_
* [runtime] `SpiderLegPrototype::lower_leg_dying_trigger_effects::effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `SpiderLegPrototype::lower_leg_dying_trigger_effects::effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `SpiderUnitPrototype::spider_engine::legs` - union collapsed to object  
  _(SpiderLegSpecification|array[SpiderLegSpecification])_
* [runtime] `SplitterPrototype::default_input_left_condition::second` - union collapsed to object  
  _(SignalIDConnector|int32)_
* [runtime] `TechnologyPrototype::unit::ingredients` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[ItemID,uint16]_
* [runtime] `ThrusterPrototype::plumes::stateless_visualisations` - union collapsed to object  
  _(PlumeEffect|array[PlumeEffect])_
* [runtime] `TileEffectDefinition::water::animation_scale` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `TileEffectDefinition::water::animation_scale` - union collapsed to object  
  _(float|[float,float])_
* [runtime] `TileEffectDefinition::water::dark_threshold` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `TileEffectDefinition::water::dark_threshold` - union collapsed to object  
  _(float|[float,float])_
* [runtime] `TileEffectDefinition::water::reflection_threshold` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `TileEffectDefinition::water::reflection_threshold` - union collapsed to object  
  _(float|[float,float])_
* [runtime] `TileEffectDefinition::water::specular_threshold` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `TileEffectDefinition::water::specular_threshold` - union collapsed to object  
  _(float|[float,float])_
* [runtime] `TurretPrototype::graphics_set::base_visualisation` - union collapsed to object  
  _(TurretBaseVisualisation|array[TurretBaseVisualisation])_
* [runtime] `AmbientSound::variable_sound::layers::sublayer_starting_offset` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `AmbientSound::variable_sound::layers::sublayer_starting_offset` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `AmbientSound::variable_sound::layers::sublayer_starting_offset` - union collapsed to object  
  _(RandomRange|ProbabilityTable)_
* [runtime] `AmbientSound::variable_sound::layers::sublayer_offset` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `AmbientSound::variable_sound::layers::sublayer_offset` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `AmbientSound::variable_sound::layers::sublayer_offset` - union collapsed to object  
  _(RandomRange|ProbabilityTable)_
* [runtime] `AmbientSound::variable_sound::layers::sample_length` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `AmbientSound::variable_sound::states::start_pause` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `AmbientSound::variable_sound::states::end_pause` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `AmbientSound::variable_sound::states::number_of_enabled_layers` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `BoilerPrototype::fluid_box::pipe_connections::positions` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[MapPosition,MapPosition,MapPosition,MapPosition]_
* [runtime] `BoilerPrototype::fluid_box::pipe_connections::connection_category` - union collapsed to object  
  _(string|array[string])_
* [runtime] `CargoBayPrototype::graphics_set::connections::tileset_mapping` - union collapsed to object  
  _(uint8|array[uint8])_
* [runtime] `PlanetPrototype::persistent_ambient_sounds::crossfade::order` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[(literal(string:wind)|literal(string:base_ambience)),(literal(string:wind)|literal(string:base_ambience))]_
* [runtime] `RailPrototype::fence_pictures::side_A::ends` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[RailFenceDirectionSet,RailFenceDirectionSet,RailFenceDirectionSet,RailFenceDirectionSet]_
* [runtime] `RailPrototype::fence_pictures::side_A::ends_upper` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[RailFenceDirectionSet,RailFenceDirectionSet,RailFenceDirectionSet,RailFenceDirectionSet]_
* [runtime] `SpiderUnitPrototype::spider_engine::legs::leg_hit_the_ground_trigger` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `SpiderUnitPrototype::spider_engine::legs::leg_hit_the_ground_trigger` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `SpiderUnitPrototype::spider_engine::legs::leg_hit_the_ground_when_attacking_trigger` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `SpiderUnitPrototype::spider_engine::legs::leg_hit_the_ground_when_attacking_trigger` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `AgriculturalTowerPrototype::crane::parts::dying_effect::particle_effects` - union collapsed to object  
  _(CreateParticleTriggerEffectItem|array[CreateParticleTriggerEffectItem])_
* [runtime] `AmbientSound::variable_sound::states::layers_properties::sequence_length` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `AmbientSound::variable_sound::states::layers_properties::number_of_repetitions` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `AmbientSound::variable_sound::states::layers_properties::number_of_repetitions` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `AmbientSound::variable_sound::states::layers_properties::number_of_repetitions` - union collapsed to object  
  _(RandomRange|ProbabilityTable)_
* [runtime] `AmbientSound::variable_sound::states::layers_properties::start_pause` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `AmbientSound::variable_sound::states::layers_properties::pause_between_samples` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `AmbientSound::variable_sound::states::layers_properties::pause_between_repetitions` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `AmbientSound::variable_sound::states::layers_properties::end_pause` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `ActiveDefenseEquipmentPrototype::attack_parameters::projectile_creation_parameters` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[RealOrientation,Vector]_
* [runtime] `ActiveDefenseEquipmentPrototype::attack_parameters::projectile_creation_parameters` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[RealOrientation,Vector]_
* [runtime] `ActiveDefenseEquipmentPrototype::attack_parameters::gun_center_shift` - union collapsed to object  
  _(Vector|GunShift4Way)_
* [runtime] `AgriculturalTowerPrototype::harvesting_sound::variations` - union collapsed to object  
  _(SoundDefinition|array[SoundDefinition])_
* [runtime] `AgriculturalTowerPrototype::harvesting_sound::modifiers` - union collapsed to object  
  _(SoundModifier|array[SoundModifier])_
* [runtime] `AmmoItemPrototype::ammo_type::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `AmmoItemPrototype::ammo_type::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ArtilleryFlarePrototype::ended_in_water_trigger_effect::non_colliding_fail_result` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ArtilleryFlarePrototype::ended_in_water_trigger_effect::non_colliding_fail_result` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ArtilleryFlarePrototype::ended_in_water_trigger_effect::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ArtilleryFlarePrototype::ended_in_water_trigger_effect::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ArtilleryProjectilePrototype::action::range_effects` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `ArtilleryProjectilePrototype::action::range_effects` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `AsteroidChunkPrototype::graphics_set::variations` - union collapsed to object  
  _(AsteroidVariation|array[AsteroidVariation])_
* [runtime] `AsteroidChunkPrototype::graphics_set::lights` - union collapsed to object  
  _(LightProperties|array[LightProperties])_
* [runtime] `AsteroidChunkPrototype::minable::mining_trigger` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `AsteroidChunkPrototype::minable::mining_trigger` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `DecorativePrototype::autoplace::tile_restriction` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[TileID,TileID]_
* [runtime] `DecorativePrototype::stateless_visualisation::offset_x` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `DecorativePrototype::stateless_visualisation::offset_y` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `DecorativePrototype::stateless_visualisation::offset_z` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `DecorativePrototype::stateless_visualisation::speed_x` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `DecorativePrototype::stateless_visualisation::speed_y` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `DecorativePrototype::stateless_visualisation::speed_z` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `DecorativePrototype::stateless_visualisation::scale` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `DecorativePrototype::stateless_visualisation::nested_visualisations` - union collapsed to object  
  _(StatelessVisualisation|array[StatelessVisualisation])_
* [runtime] `EntityPrototype::working_sound::main_sounds` - union collapsed to object  
  _(MainSound|array[MainSound])_
* [runtime] `EntityPrototype::working_sound::sound_accents` - union collapsed to object  
  _(SoundAccent|array[SoundAccent])_
* [runtime] `EntityWithHealthPrototype::attack_reaction::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `EntityWithHealthPrototype::attack_reaction::action` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ItemPrototype::spoil_to_trigger_result::trigger` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `ItemPrototype::spoil_to_trigger_result::trigger` - union collapsed to object  
  _(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)_
* [runtime] `PlanetPrototype::persistent_ambient_sounds::base_ambience` - union collapsed to object  
  _(PersistentWorldAmbientSoundDefinition|array[PersistentWorldAmbientSoundDefinition])_
* [runtime] `PlanetPrototype::persistent_ambient_sounds::wind` - union collapsed to object  
  _(PersistentWorldAmbientSoundDefinition|array[PersistentWorldAmbientSoundDefinition])_
* [runtime] `PlanetPrototype::persistent_ambient_sounds::semi_persistent` - union collapsed to object  
  _(SemiPersistentWorldAmbientSoundDefinition|array[SemiPersistentWorldAmbientSoundDefinition])_
* [runtime] `PlanetPrototype::surface_render_parameters::day_night_cycle_color_lookup` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[double,ColorLookupTable]_
* [runtime] `SegmentPrototype::update_effects::effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `SegmentPrototype::update_effects::effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `SoundPrototype::variations::modifiers` - union collapsed to object  
  _(SoundModifier|array[SoundModifier])_
* [runtime] `SpiderLegPrototype::lower_leg_dying_trigger_effects::effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `SpiderLegPrototype::lower_leg_dying_trigger_effects::effect` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `SpiderUnitPrototype::spider_engine::legs` - union collapsed to object  
  _(SpiderLegSpecification|array[SpiderLegSpecification])_
* [runtime] `SplitterPrototype::default_input_left_condition::second` - union collapsed to object  
  _(SignalIDConnector|int32)_
* [runtime] `TechnologyPrototype::unit::ingredients` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[ItemID,uint16]_
* [runtime] `ThrusterPrototype::plumes::stateless_visualisations` - union collapsed to object  
  _(PlumeEffect|array[PlumeEffect])_
* [runtime] `TileEffectDefinition::water::animation_scale` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `TileEffectDefinition::water::animation_scale` - union collapsed to object  
  _(float|[float,float])_
* [runtime] `TileEffectDefinition::water::dark_threshold` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `TileEffectDefinition::water::dark_threshold` - union collapsed to object  
  _(float|[float,float])_
* [runtime] `TileEffectDefinition::water::reflection_threshold` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `TileEffectDefinition::water::reflection_threshold` - union collapsed to object  
  _(float|[float,float])_
* [runtime] `TileEffectDefinition::water::specular_threshold` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[float,float]_
* [runtime] `TileEffectDefinition::water::specular_threshold` - union collapsed to object  
  _(float|[float,float])_
* [runtime] `TurretPrototype::graphics_set::base_visualisation` - union collapsed to object  
  _(TurretBaseVisualisation|array[TurretBaseVisualisation])_
* [runtime] `AmbientSound::variable_sound::layers::sublayer_starting_offset` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `AmbientSound::variable_sound::layers::sublayer_starting_offset` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `AmbientSound::variable_sound::layers::sublayer_starting_offset` - union collapsed to object  
  _(RandomRange|ProbabilityTable)_
* [runtime] `AmbientSound::variable_sound::layers::sublayer_offset` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `AmbientSound::variable_sound::layers::sublayer_offset` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `AmbientSound::variable_sound::layers::sublayer_offset` - union collapsed to object  
  _(RandomRange|ProbabilityTable)_
* [runtime] `AmbientSound::variable_sound::layers::sample_length` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `AmbientSound::variable_sound::states::start_pause` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `AmbientSound::variable_sound::states::end_pause` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `AmbientSound::variable_sound::states::number_of_enabled_layers` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `BoilerPrototype::fluid_box::pipe_connections::positions` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[MapPosition,MapPosition,MapPosition,MapPosition]_
* [runtime] `BoilerPrototype::fluid_box::pipe_connections::connection_category` - union collapsed to object  
  _(string|array[string])_
* [runtime] `CargoBayPrototype::graphics_set::connections::tileset_mapping` - union collapsed to object  
  _(uint8|array[uint8])_
* [runtime] `PlanetPrototype::persistent_ambient_sounds::crossfade::order` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[(literal(string:wind)|literal(string:base_ambience)),(literal(string:wind)|literal(string:base_ambience))]_
* [runtime] `RailPrototype::fence_pictures::side_A::ends` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[RailFenceDirectionSet,RailFenceDirectionSet,RailFenceDirectionSet,RailFenceDirectionSet]_
* [runtime] `RailPrototype::fence_pictures::side_A::ends_upper` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[RailFenceDirectionSet,RailFenceDirectionSet,RailFenceDirectionSet,RailFenceDirectionSet]_
* [runtime] `SpiderUnitPrototype::spider_engine::legs::leg_hit_the_ground_trigger` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `SpiderUnitPrototype::spider_engine::legs::leg_hit_the_ground_trigger` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `SpiderUnitPrototype::spider_engine::legs::leg_hit_the_ground_when_attacking_trigger` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `SpiderUnitPrototype::spider_engine::legs::leg_hit_the_ground_when_attacking_trigger` - union collapsed to object  
  _(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)_
* [runtime] `AgriculturalTowerPrototype::crane::parts::dying_effect::particle_effects` - union collapsed to object  
  _(CreateParticleTriggerEffectItem|array[CreateParticleTriggerEffectItem])_
* [runtime] `AmbientSound::variable_sound::states::layers_properties::sequence_length` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `AmbientSound::variable_sound::states::layers_properties::number_of_repetitions` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `AmbientSound::variable_sound::states::layers_properties::number_of_repetitions` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `AmbientSound::variable_sound::states::layers_properties::number_of_repetitions` - union collapsed to object  
  _(RandomRange|ProbabilityTable)_
* [runtime] `AmbientSound::variable_sound::states::layers_properties::start_pause` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `AmbientSound::variable_sound::states::layers_properties::pause_between_samples` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `AmbientSound::variable_sound::states::layers_properties::pause_between_repetitions` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `AmbientSound::variable_sound::states::layers_properties::end_pause` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[uint8,uint8]_
* [runtime] `AmbientSound::variable_sound::states::next_states::conditions::layer_sample` - tuple type has no C# representation here, mapped to LuaArray<object>  
  _[string,uint32]_

## info (4434)

* [runtime] `LuaBootstrap::get_event_filter::event` - concept 'LuaEventType' accepts several incompatible value kinds; mapped to object  
  _(LuaCustomEventPrototype|LuaCustomInputPrototype|defines.events|string)_
* [runtime] `LuaBootstrap::get_event_handler::event` - concept 'LuaEventType' accepts several incompatible value kinds; mapped to object  
  _(LuaCustomEventPrototype|LuaCustomInputPrototype|defines.events|string)_
* [runtime] `LuaBootstrap::get_event_id::event` - concept 'LuaEventType' accepts several incompatible value kinds; mapped to object  
  _(LuaCustomEventPrototype|LuaCustomInputPrototype|defines.events|string)_
* [runtime] `LuaBootstrap::get_event_name::event` - concept 'LuaEventType' accepts several incompatible value kinds; mapped to object  
  _(LuaCustomEventPrototype|LuaCustomInputPrototype|defines.events|string)_
* [runtime] `LuaBootstrap::on_event::event` - concept 'LuaEventType' accepts several incompatible value kinds; mapped to object  
  _(LuaCustomEventPrototype|LuaCustomInputPrototype|defines.events|string)_
* [runtime] `LuaBootstrap::on_event::event` - concept 'LuaEventType' accepts several incompatible value kinds; mapped to object  
  _(LuaCustomEventPrototype|LuaCustomInputPrototype|defines.events|string)_
* [runtime] `LuaBootstrap::raise_event::event` - concept 'LuaEventType' accepts several incompatible value kinds; mapped to object  
  _(LuaCustomEventPrototype|LuaCustomInputPrototype|defines.events|string)_
* [runtime] `LuaBootstrap::raise_event::data` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaBootstrap::register_metatable::metatable` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaBootstrap::register_on_object_destroyed::object` - concept 'RegistrationTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|LuaEquipment|LuaEquipmentGrid|LuaItem|LuaLogisticCell|LuaLogisticNetwork|LuaLogisticSection|LuaPermissionGroup|LuaPlanet|LuaPlayer|LuaRailPath|LuaRenderObject|LuaSpacePlatform|LuaSurface|LuaTrain|LuaCommandable|LuaCustomChartTag|LuaGuiElement|LuaCargoHatch|LuaSchedule|LuaTerritory|LuaSegmentedUnit|LuaElectricNetwork|LuaElectricSubNetwork|LuaForce)_
* [runtime] `LuaBootstrap::set_event_filter::event` - concept 'LuaEventType' accepts several incompatible value kinds; mapped to object  
  _(LuaCustomEventPrototype|LuaCustomInputPrototype|defines.events|string)_
* [runtime] `LuaBurner::currently_burning` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaCargoHatch::create_cargo_pod::cargo_pod_prototype` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaCommandProcessor::commands` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaCommandProcessor::commands` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaCommandProcessor::game_commands` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaCommandProcessor::game_commands` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaCommandProcessor::add_command::help` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaCommandProcessor::add_command::help` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaControl::cursor_ghost` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaControl::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaControl::can_insert::items` - concept 'ItemStackIdentification' accepts several incompatible value kinds; mapped to object  
  _(string|ItemStackDefinition|LuaItemStack)_
* [runtime] `LuaControl::get_craftable_count::recipe` - concept 'RecipeID' accepts several incompatible value kinds; mapped to object  
  _(LuaRecipePrototype|LuaRecipe|string)_
* [runtime] `LuaControl::insert::items` - concept 'ItemStackIdentification' accepts several incompatible value kinds; mapped to object  
  _(string|ItemStackDefinition|LuaItemStack)_
* [runtime] `LuaControl::open_factoriopedia_gui::prototype` - concept 'FactoriopediaID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemPrototype|LuaFluidPrototype|LuaRecipePrototype|LuaEntityPrototype|LuaTilePrototype|LuaSpaceLocationPrototype|LuaTechnologyPrototype|LuaAmmoCategoryPrototype|LuaSpaceConnectionPrototype|LuaAsteroidChunkPrototype|LuaVirtualSignalPrototype|LuaSurfacePrototype)_
* [runtime] `LuaControl::open_technology_gui::technology` - concept 'TechnologyID' accepts several incompatible value kinds; mapped to object  
  _(LuaTechnologyPrototype|LuaTechnology|string)_
* [runtime] `LuaControl::remove_item::items` - concept 'ItemStackIdentification' accepts several incompatible value kinds; mapped to object  
  _(string|ItemStackDefinition|LuaItemStack)_
* [runtime] `LuaControl::set_gui_arrow` - variant parameter groups merged into one optional-parameter table class  
  _crafting_queue; entity; item_stack; position_
* [runtime] `LuaControl::teleport::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaCustomChartTag::last_user` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaDebugAdapter::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaDebugAdapter::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaDebugAdapter::breakpoint::mesg` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaDebugAdapter::breakpoint::mesg` - concept 'Any' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table|LuaObject)_
* [runtime] `LuaDebugAdapter::describe_field::value` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaDebugAdapter::describe_field::value` - concept 'Any' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table|LuaObject)_
* [runtime] `LuaElectricEnergySourcePrototype::get_input_flow_limit::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaElectricEnergySourcePrototype::get_output_flow_limit::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntity::associated_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaEntity::character_corpse_death_cause` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaEntity::character_corpse_death_cause` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaEntity::character_corpse_death_cause` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaEntity::character_corpse_death_cause` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaEntity::ghost_localised_description` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaEntity::ghost_localised_description` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaEntity::ghost_localised_name` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaEntity::ghost_localised_name` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaEntity::last_user` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaEntity::localised_description` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaEntity::localised_description` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaEntity::localised_name` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaEntity::localised_name` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaEntity::render_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaEntity::render_to_forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaEntity::render_to_forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaEntity::render_to_forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaEntity::result_quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntity::storage_filter` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaEntity::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaEntity::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaEntity::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaEntity::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaEntity::apply_upgrade::override_target` - concept 'EntityWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|LuaEntityPrototype|string|EntityIDAndQualityIDPair)_
* [runtime] `LuaEntity::cancel_deconstruction::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaEntity::cancel_deconstruction::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaEntity::cancel_upgrade::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaEntity::cancel_upgrade::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaEntity::copy_settings::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaEntity::damage::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaEntity::damage::type` - concept 'DamageTypeID' accepts several incompatible value kinds; mapped to object  
  _(LuaDamagePrototype|string)_
* [runtime] `LuaEntity::die::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaEntity::get_electric_input_flow_limit::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntity::get_electric_output_flow_limit::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntity::get_filter::return` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaEntity::get_filter::return` - concept 'AsteroidChunkID' accepts several incompatible value kinds; mapped to object  
  _(LuaAsteroidChunkPrototype|string)_
* [runtime] `LuaEntity::get_stored_durability::item` - concept 'ItemID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemPrototype|LuaItemStack|LuaItem|string)_
* [runtime] `LuaEntity::is_registered_for_deconstruction::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaEntity::order_deconstruction::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaEntity::order_deconstruction::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaEntity::request_to_close::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaEntity::request_to_open::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaEntity::set_driver::driver` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaEntity::set_filter::filter` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaEntity::set_filter::filter` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaEntity::set_filter::filter` - concept 'AsteroidChunkID' accepts several incompatible value kinds; mapped to object  
  _(LuaAsteroidChunkPrototype|string)_
* [runtime] `LuaEntity::set_fluid_filter::filter` - concept 'FluidID' accepts several incompatible value kinds; mapped to object  
  _(string|LuaFluidPrototype|Fluid)_
* [runtime] `LuaEntity::set_passenger::passenger` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaEntity::set_priority_target::entity_id` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaEntity::set_recipe::recipe` - concept 'RecipeID' accepts several incompatible value kinds; mapped to object  
  _(LuaRecipePrototype|LuaRecipe|string)_
* [runtime] `LuaEntity::set_recipe::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntity::set_stored_durability::item` - concept 'ItemID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemPrototype|LuaItemStack|LuaItem|string)_
* [runtime] `LuaEntityPrototype::crafting_speed_quality_multiplier` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntityPrototype::energy_usage_quality_multiplier` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntityPrototype::module_slots_quality_bonus` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntityPrototype::get_attraction_range_elongation::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntityPrototype::get_crafting_speed::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntityPrototype::get_duration::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntityPrototype::get_energy_distribution_efficiency::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntityPrototype::get_fluid_capacity::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntityPrototype::get_fluid_usage_per_tick::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntityPrototype::get_inserter_extension_speed::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntityPrototype::get_inserter_rotation_speed::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntityPrototype::get_inventory_size::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntityPrototype::get_inventory_size_bonus::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntityPrototype::get_max_circuit_wire_distance::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntityPrototype::get_max_distance_of_nearby_sector_revealed::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntityPrototype::get_max_distance_of_sector_revealed::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntityPrototype::get_max_energy::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntityPrototype::get_max_energy_production::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntityPrototype::get_max_energy_usage::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntityPrototype::get_max_health::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntityPrototype::get_max_power_output::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntityPrototype::get_max_speed::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntityPrototype::get_max_wire_distance::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntityPrototype::get_mining_drill_radius::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntityPrototype::get_pumping_speed::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntityPrototype::get_researching_speed::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntityPrototype::get_supply_area_distance::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntityPrototype::get_valve_flow_rate::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEquipmentGrid::clear::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaEquipmentGrid::count::equipment` - concept 'EquipmentWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEquipmentPrototype|LuaEquipment|string|EquipmentIDAndQualityIDPair)_
* [runtime] `LuaEquipmentGrid::find::equipment` - concept 'EquipmentWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEquipmentPrototype|LuaEquipment|string|EquipmentIDAndQualityIDPair)_
* [runtime] `LuaEquipmentGrid::get_generator_energy::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEquipmentGrid::take_all::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaEquipmentPrototype::get_energy_consumption::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEquipmentPrototype::get_inventory_bonus::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEquipmentPrototype::get_movement_bonus::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEquipmentPrototype::get_shield::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaFlowStatistics::get_current_input_sample::id` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaFlowStatistics::get_current_input_sample::id` - concept 'FluidID' accepts several incompatible value kinds; mapped to object  
  _(string|LuaFluidPrototype|Fluid)_
* [runtime] `LuaFlowStatistics::get_current_input_sample::id` - concept 'EntityWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|LuaEntityPrototype|string|EntityIDAndQualityIDPair)_
* [runtime] `LuaFlowStatistics::get_current_input_sample::id` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaFlowStatistics::get_current_output_sample::id` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaFlowStatistics::get_current_output_sample::id` - concept 'FluidID' accepts several incompatible value kinds; mapped to object  
  _(string|LuaFluidPrototype|Fluid)_
* [runtime] `LuaFlowStatistics::get_current_output_sample::id` - concept 'EntityWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|LuaEntityPrototype|string|EntityIDAndQualityIDPair)_
* [runtime] `LuaFlowStatistics::get_current_output_sample::id` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaFlowStatistics::get_input_count::id` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaFlowStatistics::get_input_count::id` - concept 'FluidID' accepts several incompatible value kinds; mapped to object  
  _(string|LuaFluidPrototype|Fluid)_
* [runtime] `LuaFlowStatistics::get_input_count::id` - concept 'EntityWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|LuaEntityPrototype|string|EntityIDAndQualityIDPair)_
* [runtime] `LuaFlowStatistics::get_input_count::id` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaFlowStatistics::get_output_count::id` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaFlowStatistics::get_output_count::id` - concept 'FluidID' accepts several incompatible value kinds; mapped to object  
  _(string|LuaFluidPrototype|Fluid)_
* [runtime] `LuaFlowStatistics::get_output_count::id` - concept 'EntityWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|LuaEntityPrototype|string|EntityIDAndQualityIDPair)_
* [runtime] `LuaFlowStatistics::get_output_count::id` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaFlowStatistics::get_storage_count::id` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaFlowStatistics::get_storage_count::id` - concept 'FluidID' accepts several incompatible value kinds; mapped to object  
  _(string|LuaFluidPrototype|Fluid)_
* [runtime] `LuaFlowStatistics::get_storage_count::id` - concept 'EntityWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|LuaEntityPrototype|string|EntityIDAndQualityIDPair)_
* [runtime] `LuaFlowStatistics::get_storage_count::id` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaFlowStatistics::on_flow::id` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaFlowStatistics::on_flow::id` - concept 'FluidID' accepts several incompatible value kinds; mapped to object  
  _(string|LuaFluidPrototype|Fluid)_
* [runtime] `LuaFlowStatistics::on_flow::id` - concept 'EntityWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|LuaEntityPrototype|string|EntityIDAndQualityIDPair)_
* [runtime] `LuaFlowStatistics::on_flow::id` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaFlowStatistics::set_current_input_sample::id` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaFlowStatistics::set_current_input_sample::id` - concept 'FluidID' accepts several incompatible value kinds; mapped to object  
  _(string|LuaFluidPrototype|Fluid)_
* [runtime] `LuaFlowStatistics::set_current_input_sample::id` - concept 'EntityWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|LuaEntityPrototype|string|EntityIDAndQualityIDPair)_
* [runtime] `LuaFlowStatistics::set_current_input_sample::id` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaFlowStatistics::set_current_output_sample::id` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaFlowStatistics::set_current_output_sample::id` - concept 'FluidID' accepts several incompatible value kinds; mapped to object  
  _(string|LuaFluidPrototype|Fluid)_
* [runtime] `LuaFlowStatistics::set_current_output_sample::id` - concept 'EntityWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|LuaEntityPrototype|string|EntityIDAndQualityIDPair)_
* [runtime] `LuaFlowStatistics::set_current_output_sample::id` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaFlowStatistics::set_input_count::id` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaFlowStatistics::set_input_count::id` - concept 'FluidID' accepts several incompatible value kinds; mapped to object  
  _(string|LuaFluidPrototype|Fluid)_
* [runtime] `LuaFlowStatistics::set_input_count::id` - concept 'EntityWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|LuaEntityPrototype|string|EntityIDAndQualityIDPair)_
* [runtime] `LuaFlowStatistics::set_input_count::id` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaFlowStatistics::set_output_count::id` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaFlowStatistics::set_output_count::id` - concept 'FluidID' accepts several incompatible value kinds; mapped to object  
  _(string|LuaFluidPrototype|Fluid)_
* [runtime] `LuaFlowStatistics::set_output_count::id` - concept 'EntityWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|LuaEntityPrototype|string|EntityIDAndQualityIDPair)_
* [runtime] `LuaFlowStatistics::set_output_count::id` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaFlowStatistics::set_storage_count::id` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaFlowStatistics::set_storage_count::id` - concept 'FluidID' accepts several incompatible value kinds; mapped to object  
  _(string|LuaFluidPrototype|Fluid)_
* [runtime] `LuaFlowStatistics::set_storage_count::id` - concept 'EntityWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|LuaEntityPrototype|string|EntityIDAndQualityIDPair)_
* [runtime] `LuaFlowStatistics::set_storage_count::id` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaFluidBoxPrototype::get_volume::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaForce::research_queue` - concept 'TechnologyID' accepts several incompatible value kinds; mapped to object  
  _(LuaTechnologyPrototype|LuaTechnology|string)_
* [runtime] `LuaForce::research_queue` - concept 'TechnologyID' accepts several incompatible value kinds; mapped to object  
  _(LuaTechnologyPrototype|LuaTechnology|string)_
* [runtime] `LuaForce::add_chart_tag::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaForce::add_custom_alert::message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaForce::add_custom_alert::message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaForce::add_research::technology` - concept 'TechnologyID' accepts several incompatible value kinds; mapped to object  
  _(LuaTechnologyPrototype|LuaTechnology|string)_
* [runtime] `LuaForce::cancel_charting::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaForce::chart::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaForce::chart_all::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaForce::clear_chart::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaForce::copy_chart::source_force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaForce::copy_chart::source_surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaForce::copy_chart::destination_surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaForce::copy_from::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaForce::find_chart_tags::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaForce::find_logistic_network_by_position::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaForce::get_cease_fire::other` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaForce::get_chunk_chart::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaForce::get_entity_build_count_statistics::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaForce::get_entity_count::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaForce::get_evolution_factor::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaForce::get_evolution_factor_by_killing_spawners::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaForce::get_evolution_factor_by_pollution::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaForce::get_evolution_factor_by_time::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaForce::get_fluid_production_statistics::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaForce::get_friend::other` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaForce::get_hand_crafting_disabled_for_recipe::recipe` - concept 'RecipeID' accepts several incompatible value kinds; mapped to object  
  _(LuaRecipePrototype|LuaRecipe|string)_
* [runtime] `LuaForce::get_item_launched::item` - concept 'ItemID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemPrototype|LuaItemStack|LuaItem|string)_
* [runtime] `LuaForce::get_item_production_statistics::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaForce::get_kill_count_statistics::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaForce::get_linked_inventory::prototype` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaForce::get_space_platforms::location` - concept 'SpaceLocationID' accepts several incompatible value kinds; mapped to object  
  _(LuaSpaceLocationPrototype|string)_
* [runtime] `LuaForce::get_spawn_position::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaForce::get_surface_hidden::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaForce::get_turret_attack_modifier::turret` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaForce::is_chunk_charted::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaForce::is_chunk_requested_for_charting::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaForce::is_chunk_visible::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaForce::is_enemy::other` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaForce::is_friend::other` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaForce::is_quality_unlocked::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaForce::is_space_location_unlocked::name` - concept 'SpaceLocationID' accepts several incompatible value kinds; mapped to object  
  _(LuaSpaceLocationPrototype|string)_
* [runtime] `LuaForce::lock_quality::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaForce::lock_space_location::name` - concept 'SpaceLocationID' accepts several incompatible value kinds; mapped to object  
  _(LuaSpaceLocationPrototype|string)_
* [runtime] `LuaForce::print::message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaForce::print::message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaForce::rechart::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaForce::script_trigger_research::technology` - concept 'TechnologyID' accepts several incompatible value kinds; mapped to object  
  _(LuaTechnologyPrototype|LuaTechnology|string)_
* [runtime] `LuaForce::set_cease_fire::other` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaForce::set_evolution_factor::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaForce::set_evolution_factor_by_killing_spawners::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaForce::set_evolution_factor_by_pollution::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaForce::set_evolution_factor_by_time::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaForce::set_friend::other` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaForce::set_hand_crafting_disabled_for_recipe::recipe` - concept 'RecipeID' accepts several incompatible value kinds; mapped to object  
  _(LuaRecipePrototype|LuaRecipe|string)_
* [runtime] `LuaForce::set_item_launched::item` - concept 'ItemID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemPrototype|LuaItemStack|LuaItem|string)_
* [runtime] `LuaForce::set_spawn_position::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaForce::set_surface_hidden::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaForce::set_turret_attack_modifier::turret` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaForce::unchart_chunk::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaForce::unlock_quality::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaForce::unlock_space_location::name` - concept 'SpaceLocationID' accepts several incompatible value kinds; mapped to object  
  _(LuaSpaceLocationPrototype|string)_
* [runtime] `LuaGameScript::forces` - scalar union mapped to object  
  _(uint32|string)_
* [runtime] `LuaGameScript::players` - scalar union mapped to object  
  _(uint32|string)_
* [runtime] `LuaGameScript::surfaces` - scalar union mapped to object  
  _(uint32|string)_
* [runtime] `LuaGameScript::ban_player::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaGameScript::create_inventory::gui_title` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::create_inventory::gui_title` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::delete_blueprint_library::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaGameScript::delete_surface::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaGameScript::get_player::player` - scalar union mapped to object  
  _(uint32|string)_
* [runtime] `LuaGameScript::get_pollution_statistics::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaGameScript::get_surface::surface` - scalar union mapped to object  
  _(uint32|string)_
* [runtime] `LuaGameScript::kick_player::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaGameScript::merge_forces::source` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaGameScript::merge_forces::destination` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaGameScript::mute_player::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaGameScript::print::message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::print::message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::purge_player::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaGameScript::remove_offline_players::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaGameScript::unban_player::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaGameScript::unmute_player::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaGuiElement::badge_text` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::badge_text` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::badge_text` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::badge_text` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::caption` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::caption` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::caption` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::caption` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::items` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::items` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::items` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::items` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::left_label_caption` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::left_label_caption` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::left_label_caption` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::left_label_caption` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::left_label_tooltip` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::left_label_tooltip` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::left_label_tooltip` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::left_label_tooltip` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaGuiElement::right_label_caption` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::right_label_caption` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::right_label_caption` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::right_label_caption` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::right_label_tooltip` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::right_label_tooltip` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::right_label_tooltip` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::right_label_tooltip` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaGuiElement::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaGuiElement::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaGuiElement::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaGuiElement::tooltip` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::tooltip` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::tooltip` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::tooltip` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add` - variant parameter groups merged into one optional-parameter table class  
  _button; camera; checkbox; choose-elem-button; drop-down; flow; frame; inventory; line; list-box; minimap; progressbar; radiobutton; scroll-pane; slider; sprite; sprite-button; switch; tab; table; text-box; textfield_
* [runtime] `LuaGuiElement::add_item::string` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::add_item::string` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::get_item::return` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::get_item::return` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::set_item::string` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::set_item::string` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaHelpers::json_to_table::return` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaHelpers::json_to_table::return` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaHelpers::send_udp::data` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaHelpers::send_udp::data` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaHelpers::table_to_json::data` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaHelpers::write_file::data` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaHelpers::write_file::data` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaInventory::can_insert::items` - concept 'ItemStackIdentification' accepts several incompatible value kinds; mapped to object  
  _(string|ItemStackDefinition|LuaItemStack)_
* [runtime] `LuaInventory::find_empty_stack::item` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaInventory::find_item_stack::item` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaInventory::get_insertable_count::item` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaInventory::get_item_count::item` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaInventory::get_item_quality_counts::item` - concept 'ItemID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemPrototype|LuaItemStack|LuaItem|string)_
* [runtime] `LuaInventory::insert::items` - concept 'ItemStackIdentification' accepts several incompatible value kinds; mapped to object  
  _(string|ItemStackDefinition|LuaItemStack)_
* [runtime] `LuaInventory::remove::items` - concept 'ItemStackIdentification' accepts several incompatible value kinds; mapped to object  
  _(string|ItemStackDefinition|LuaItemStack)_
* [runtime] `LuaItemCommon::custom_description` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaItemCommon::custom_description` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaItemCommon::custom_description` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaItemCommon::custom_description` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaItemCommon::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaItemCommon::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaItemCommon::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaItemCommon::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaItemCommon::tile_filters` - concept 'TileID' accepts several incompatible value kinds; mapped to object  
  _(LuaTilePrototype|LuaTile|string)_
* [runtime] `LuaItemCommon::tile_filters` - concept 'TileID' accepts several incompatible value kinds; mapped to object  
  _(LuaTilePrototype|LuaTile|string)_
* [runtime] `LuaItemCommon::get_blueprint_entity_tag::return` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaItemCommon::get_blueprint_entity_tag::return` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaItemCommon::get_blueprint_entity_tags::return` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaItemCommon::get_blueprint_entity_tags::return` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaItemCommon::get_tag::return` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaItemCommon::get_tag::return` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaItemCommon::set_blueprint_entity_tag::value` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaItemCommon::set_blueprint_entity_tag::value` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaItemCommon::set_blueprint_entity_tags::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaItemCommon::set_blueprint_entity_tags::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaItemCommon::set_tag::tag` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaItemCommon::set_tag::tag` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaItemGroup::localised_name` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaItemGroup::localised_name` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaItemPrototype::localised_filter_message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaItemPrototype::localised_filter_message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaItemPrototype::rocket_launch_products` - concept 'Product' accepts several incompatible value kinds; mapped to object  
  _(ItemProduct|FluidProduct)_
* [runtime] `LuaItemPrototype::get_durability::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaItemPrototype::get_inventory_size::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaItemPrototype::get_inventory_size_bonus::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaItemPrototype::get_module_effects::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaItemPrototype::get_spoil_ticks::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaItemStack::can_set_stack::stack` - concept 'ItemStackIdentification' accepts several incompatible value kinds; mapped to object  
  _(string|ItemStackDefinition|LuaItemStack)_
* [runtime] `LuaItemStack::set_stack::stack` - concept 'ItemStackIdentification' accepts several incompatible value kinds; mapped to object  
  _(string|ItemStackDefinition|LuaItemStack)_
* [runtime] `LuaItemStack::transfer_stack::stack` - concept 'ItemStackIdentification' accepts several incompatible value kinds; mapped to object  
  _(string|ItemStackDefinition|LuaItemStack)_
* [runtime] `LuaItemSubGroup::localised_name` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaItemSubGroup::localised_name` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaLazyLoadedValue::get::return` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaLazyLoadedValue::get::return` - concept 'Any' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table|LuaObject)_
* [runtime] `LuaLogisticNetwork::can_satisfy_request::item` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaLogisticNetwork::get_item_count::item` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaLogisticNetwork::get_supply_counts::item` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaLogisticNetwork::get_supply_points::item` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaLogisticNetwork::insert::item` - concept 'ItemStackIdentification' accepts several incompatible value kinds; mapped to object  
  _(string|ItemStackDefinition|LuaItemStack)_
* [runtime] `LuaLogisticNetwork::remove_item::item` - concept 'ItemStackIdentification' accepts several incompatible value kinds; mapped to object  
  _(string|ItemStackDefinition|LuaItemStack)_
* [runtime] `LuaModData::data` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaModData::data` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaModData::get::return` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaModData::get::return` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaNotificationQueue::add::object` - concept 'RegistrationTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|LuaEquipment|LuaEquipmentGrid|LuaItem|LuaLogisticCell|LuaLogisticNetwork|LuaLogisticSection|LuaPermissionGroup|LuaPlanet|LuaPlayer|LuaRailPath|LuaRenderObject|LuaSpacePlatform|LuaSurface|LuaTrain|LuaCommandable|LuaCustomChartTag|LuaGuiElement|LuaCargoHatch|LuaSchedule|LuaTerritory|LuaSegmentedUnit|LuaElectricNetwork|LuaElectricSubNetwork|LuaForce)_
* [runtime] `LuaNotificationQueue::find::object` - concept 'RegistrationTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|LuaEquipment|LuaEquipmentGrid|LuaItem|LuaLogisticCell|LuaLogisticNetwork|LuaLogisticSection|LuaPermissionGroup|LuaPlanet|LuaPlayer|LuaRailPath|LuaRenderObject|LuaSpacePlatform|LuaSurface|LuaTrain|LuaCommandable|LuaCustomChartTag|LuaGuiElement|LuaCargoHatch|LuaSchedule|LuaTerritory|LuaSegmentedUnit|LuaElectricNetwork|LuaElectricSubNetwork|LuaForce)_
* [runtime] `LuaPermissionGroup::add_player::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaPermissionGroup::remove_player::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaPermissionGroups::get_group::group` - scalar union mapped to object  
  _(string|uint32)_
* [runtime] `LuaPlanet::associate_surface::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaPlanet::get_space_platforms::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaPlayer::respawn_quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaPlayer::add_custom_alert::message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPlayer::add_custom_alert::message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPlayer::add_recipe_notification::recipe` - concept 'RecipeID' accepts several incompatible value kinds; mapped to object  
  _(LuaRecipePrototype|LuaRecipe|string)_
* [runtime] `LuaPlayer::clear_recipe_notification::recipe` - concept 'RecipeID' accepts several incompatible value kinds; mapped to object  
  _(LuaRecipePrototype|LuaRecipe|string)_
* [runtime] `LuaPlayer::create_character::character` - concept 'EntityWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|LuaEntityPrototype|string|EntityIDAndQualityIDPair)_
* [runtime] `LuaPlayer::get_goal_description::return` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPlayer::get_goal_description::return` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPlayer::pipette::id` - concept 'PipetteID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEquipmentPrototype|LuaFluidPrototype|LuaItemPrototype|LuaRecipePrototype|LuaSpaceLocationPrototype|LuaTilePrototype|LuaVirtualSignalPrototype)_
* [runtime] `LuaPlayer::pipette::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaPlayer::print::message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPlayer::print::message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPlayer::print_entity_statistics::entities` - concept 'EntityWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|LuaEntityPrototype|string|EntityIDAndQualityIDPair)_
* [runtime] `LuaPlayer::request_translation::localised_string` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPlayer::request_translation::localised_string` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPlayer::request_translations::localised_strings` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPlayer::request_translations::localised_strings` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPlayer::set_ending_screen_data::message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPlayer::set_ending_screen_data::message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPlayer::set_goal_description::text` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPlayer::set_goal_description::text` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPlayer::set_quick_bar_slot::filter` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaPlayer::swap_characters::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaPrototypeBase::factoriopedia_description` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPrototypeBase::factoriopedia_description` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPrototypeBase::localised_description` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPrototypeBase::localised_description` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPrototypeBase::localised_name` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPrototypeBase::localised_name` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaQualityPrototype::get_roll_chances::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaQualityPrototype::roll_quality::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRCON::print::message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaRCON::print::message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaRailEnd::get_rail_extensions::planner_item` - concept 'ItemID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemPrototype|LuaItemStack|LuaItem|string)_
* [runtime] `LuaRecipe::localised_description` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaRecipe::localised_description` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaRecipe::localised_name` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaRecipe::localised_name` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaRecipe::products` - concept 'Product' accepts several incompatible value kinds; mapped to object  
  _(ItemProduct|FluidProduct)_
* [runtime] `LuaRecipe::has_category::category` - concept 'RecipeCategoryID' accepts several incompatible value kinds; mapped to object  
  _(LuaRecipeCategoryPrototype|string)_
* [runtime] `LuaRecipePrototype::effect_limitation_messages` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaRecipePrototype::effect_limitation_messages` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaRecipePrototype::main_product` - concept 'Product' accepts several incompatible value kinds; mapped to object  
  _(ItemProduct|FluidProduct)_
* [runtime] `LuaRecipePrototype::on_crafted_event` - concept 'LuaEventType' accepts several incompatible value kinds; mapped to object  
  _(LuaCustomEventPrototype|LuaCustomInputPrototype|defines.events|string)_
* [runtime] `LuaRecipePrototype::products` - concept 'Product' accepts several incompatible value kinds; mapped to object  
  _(ItemProduct|FluidProduct)_
* [runtime] `LuaRecipePrototype::get_ingredient_quality::recipe_quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaRecipePrototype::get_product_quality::recipe_quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaRecipePrototype::has_category::category` - concept 'RecipeCategoryID' accepts several incompatible value kinds; mapped to object  
  _(LuaRecipeCategoryPrototype|string)_
* [runtime] `LuaRecord::tile_filters` - concept 'TileID' accepts several incompatible value kinds; mapped to object  
  _(LuaTilePrototype|LuaTile|string)_
* [runtime] `LuaRecord::tile_filters` - concept 'TileID' accepts several incompatible value kinds; mapped to object  
  _(LuaTilePrototype|LuaTile|string)_
* [runtime] `LuaRecord::get_active_index::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRecord::get_blueprint_entity_tag::return` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaRecord::get_blueprint_entity_tag::return` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaRecord::get_blueprint_entity_tags::return` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaRecord::get_blueprint_entity_tags::return` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaRecord::get_selected_record::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRecord::set_blueprint_entity_tag::value` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaRecord::set_blueprint_entity_tag::value` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaRecord::set_blueprint_entity_tags::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaRecord::set_blueprint_entity_tags::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaRemote::call::return` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaRemote::call::return` - concept 'Any' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table|LuaObject)_
* [runtime] `LuaRenderObject::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRenderObject::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRenderObject::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRenderObject::from` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRenderObject::from` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRenderObject::left_top` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRenderObject::left_top` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRenderObject::orientation_target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRenderObject::orientation_target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRenderObject::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRenderObject::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRenderObject::right_bottom` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRenderObject::right_bottom` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRenderObject::target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRenderObject::target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRenderObject::text` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaRenderObject::text` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaRenderObject::text` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaRenderObject::text` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaRenderObject::to` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRenderObject::to` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRenderObject::vertices` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRenderObject::vertices` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRenderObject::set_corners::left_top` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRenderObject::set_corners::right_bottom` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaSegmentedUnit::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSegmentedUnit::damage::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSegmentedUnit::damage::type` - concept 'DamageTypeID' accepts several incompatible value kinds; mapped to object  
  _(LuaDamagePrototype|string)_
* [runtime] `LuaSegmentedUnit::die::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSegmentedUnit::die::damage_type` - concept 'DamageTypeID' accepts several incompatible value kinds; mapped to object  
  _(LuaDamagePrototype|string)_
* [runtime] `LuaSettings::get_player_settings::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaSimulation::camera_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaSimulation::get_widget_position` - variant parameter groups merged into one optional-parameter table class  
  _check-box; drop-down; item-group-tab; label; logistics-button; logistics-button-space; quickbar-slot; recipe-slot; signal-id-base; simple-item-slot; simple-item-with-quality-slot; simple-slot; text-button; text-button-localised-substring; text-button-substring; textfield_
* [runtime] `LuaSpacePlatform::space_connection` - concept 'SpaceConnectionID' accepts several incompatible value kinds; mapped to object  
  _(LuaSpaceConnectionPrototype|string)_
* [runtime] `LuaSpacePlatform::space_location` - concept 'SpaceLocationID' accepts several incompatible value kinds; mapped to object  
  _(LuaSpaceLocationPrototype|string)_
* [runtime] `LuaSpacePlatform::eject_item::item` - concept 'ItemStackIdentification' accepts several incompatible value kinds; mapped to object  
  _(string|ItemStackDefinition|LuaItemStack)_
* [runtime] `LuaSurface::localised_name` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaSurface::localised_name` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaSurface::localised_name` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaSurface::localised_name` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaSurface::build_enemy_base::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::create_entity` - variant parameter groups merged into one optional-parameter table class  
  _accumulator; agricultural-tower; ammo-turret; arithmetic-combinator; artillery-flare; artillery-projectile; artillery-turret; artillery-wagon; assembling-machine; asteroid; asteroid-collector; beam; boiler; car; cargo-landing-pad; cargo-wagon; character-corpse; cliff; constant-combinator; container; decider-combinator; deconstructible-tile-proxy; display-panel; electric-energy-interface; electric-pole; electric-turret; entity-ghost; fire; fluid-turret; fluid-wagon; furnace; heat-interface; heat-pipe; highlight-box; infinity-cargo-wagon; infinity-container; infinity-pipe; inserter; item-entity; item-request-proxy; lab; lamp; land-mine; lane-splitter; linked-belt; linked-container; loader; loader-1x1; locomotive; logistic-container; mining-drill; offshore-pump; particle; pipe; pipe-to-ground; plant; power-switch; programmable-speaker; projectile; proxy-container; pump; radar; rail-chain-signal; rail-signal; reactor; resource; roboport; rocket-silo; selector-combinator; simple-entity-with-force; simple-entity-with-owner; space-platform-hub; speech-bubble; spider-vehicle; splitter; storage-tank; stream; tile-ghost; train-stop; transport-belt; turret; underground-belt; valve; wall_
* [runtime] `LuaSurface::create_segmented_unit` - variant parameter groups merged into one optional-parameter table class  
  _body-nodes; position-and-direction_
* [runtime] `LuaSurface::decorative_prototype_collides::prototype` - concept 'DecorativeID' accepts several incompatible value kinds; mapped to object  
  _(LuaDecorativePrototype|string)_
* [runtime] `LuaSurface::entity_prototype_collides::prototype` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::find_closest_logistic_network_by_position::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::find_enemy_units::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::find_entity::entity` - concept 'EntityWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|LuaEntityPrototype|string|EntityIDAndQualityIDPair)_
* [runtime] `LuaSurface::find_logistic_network_by_position::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::find_logistic_networks_by_construction_area::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::find_non_colliding_position::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::find_non_colliding_position_in_box::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::get_connected_tiles::tiles` - concept 'TileID' accepts several incompatible value kinds; mapped to object  
  _(LuaTilePrototype|LuaTile|string)_
* [runtime] `LuaSurface::get_default_cover_tile::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::get_default_cover_tile::tile` - concept 'TileID' accepts several incompatible value kinds; mapped to object  
  _(LuaTilePrototype|LuaTile|string)_
* [runtime] `LuaSurface::get_entities_with_force::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::get_property::property` - concept 'SurfacePropertyID' accepts several incompatible value kinds; mapped to object  
  _(LuaSurfacePropertyPrototype|string)_
* [runtime] `LuaSurface::get_script_area::key` - scalar union mapped to object  
  _(string|uint32)_
* [runtime] `LuaSurface::get_script_position::key` - scalar union mapped to object  
  _(string|uint32)_
* [runtime] `LuaSurface::pollute::prototype` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::print::message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaSurface::print::message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaSurface::set_default_cover_tile::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::set_default_cover_tile::from_tile` - concept 'TileID' accepts several incompatible value kinds; mapped to object  
  _(LuaTilePrototype|LuaTile|string)_
* [runtime] `LuaSurface::set_default_cover_tile::to_tile` - concept 'TileID' accepts several incompatible value kinds; mapped to object  
  _(LuaTilePrototype|LuaTile|string)_
* [runtime] `LuaSurface::set_double_hidden_tile::tile` - concept 'TileID' accepts several incompatible value kinds; mapped to object  
  _(LuaTilePrototype|LuaTile|string)_
* [runtime] `LuaSurface::set_hidden_tile::tile` - concept 'TileID' accepts several incompatible value kinds; mapped to object  
  _(LuaTilePrototype|LuaTile|string)_
* [runtime] `LuaSurface::set_property::property` - concept 'SurfacePropertyID' accepts several incompatible value kinds; mapped to object  
  _(LuaSurfacePropertyPrototype|string)_
* [runtime] `LuaSurface::set_tiles::remove_colliding_entities` - scalar union mapped to object  
  _(boolean|literal(string:abort_on_collision))_
* [runtime] `LuaSurface::set_tiles::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaSurfacePrototype::surface_properties` - concept 'SurfacePropertyID' accepts several incompatible value kinds; mapped to object  
  _(LuaSurfacePropertyPrototype|string)_
* [runtime] `LuaTechnology::localised_description` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaTechnology::localised_description` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaTechnology::localised_name` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaTechnology::localised_name` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaTile::cancel_deconstruction::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaTile::cancel_deconstruction::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaTile::get_tile_ghosts::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaTile::has_tile_ghost::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaTile::order_deconstruction::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaTile::order_deconstruction::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaTile::to_be_deconstructed::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaTrain::insert::stack` - concept 'ItemStackIdentification' accepts several incompatible value kinds; mapped to object  
  _(string|ItemStackDefinition|LuaItemStack)_
* [runtime] `LuaTrain::remove_item::stack` - concept 'ItemStackIdentification' accepts several incompatible value kinds; mapped to object  
  _(string|ItemStackDefinition|LuaItemStack)_
* [runtime] `LuaTransportLine::force_insert_at::items` - concept 'ItemStackIdentification' accepts several incompatible value kinds; mapped to object  
  _(string|ItemStackDefinition|LuaItemStack)_
* [runtime] `LuaTransportLine::insert_at::items` - concept 'ItemStackIdentification' accepts several incompatible value kinds; mapped to object  
  _(string|ItemStackDefinition|LuaItemStack)_
* [runtime] `LuaTransportLine::insert_at_back::items` - concept 'ItemStackIdentification' accepts several incompatible value kinds; mapped to object  
  _(string|ItemStackDefinition|LuaItemStack)_
* [runtime] `LuaTransportLine::remove_item::items` - concept 'ItemStackIdentification' accepts several incompatible value kinds; mapped to object  
  _(string|ItemStackDefinition|LuaItemStack)_
* [runtime] `LuaUndoRedoStack::get_redo_tag::return` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaUndoRedoStack::get_redo_tag::return` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaUndoRedoStack::get_redo_tags::return` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaUndoRedoStack::get_redo_tags::return` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaUndoRedoStack::get_undo_tag::return` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaUndoRedoStack::get_undo_tag::return` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaUndoRedoStack::get_undo_tags::return` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaUndoRedoStack::get_undo_tags::return` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaUndoRedoStack::set_redo_tag::tag` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaUndoRedoStack::set_redo_tag::tag` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaUndoRedoStack::set_undo_tag::tag` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaUndoRedoStack::set_undo_tag::tag` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `global::localised_print::string` - recursive concept 'LocalisedString' mapped to object
* [runtime] `global::localised_print::string` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `global::log::string` - recursive concept 'LocalisedString' mapped to object
* [runtime] `global::log::string` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `global::table_size::table` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `event CustomInputEvent` - event has no `defines.events` entry; only the event data class is generated
* [runtime] `event RecipeCraftedEvent` - event has no `defines.events` entry; only the event data class is generated
* [runtime] `LuaAccumulatorControlBehavior::output_signal::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::entity_name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::entity_name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::entity_name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::damage` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::radius` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offsets` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offsets` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offsets` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offsets` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offsets` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offsets` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::radius` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::radius` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offset_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offset_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offset_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offset_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offset_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offset_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::trigger_created_entity` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::trigger_created_entity` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::trigger_created_entity` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::tile_collision_mask` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::trigger_created_entity` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::check_buildability` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::check_buildability` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::initial_height` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::check_buildability` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::tile_collision_mask` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::max_distance` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::only_when_visible` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::only_when_visible` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::only_when_visible` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::apply_projection` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::tile_collision_mask` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::tile_collision_mask` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::tile_collision_mask` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::tile_collision_mask` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::as_enemy` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::as_enemy` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::as_enemy` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::ignore_no_enemies_mode` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::ignore_no_enemies_mode` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::ignore_no_enemies_mode` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::protected` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::protected` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::protected` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::find_non_colliding_position` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::find_non_colliding_position` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::find_non_colliding_position` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::speed_from_center` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::abort_if_over_space` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::abort_if_over_space` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::abort_if_over_space` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::speed_from_center_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::non_colliding_search_radius` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::non_colliding_search_radius` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::non_colliding_search_radius` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::non_colliding_search_precision` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::non_colliding_search_precision` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::non_colliding_search_precision` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::non_colliding_fail_result` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::non_colliding_fail_result` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::non_colliding_fail_result` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::initial_height` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::speed` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::only_when_visible` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::speed_multiplier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::speed_multiplier_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::starting_frame` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::starting_frame_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::speed_from_center` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::speed_from_center_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::mineable_properties::products` - concept 'Product' accepts several incompatible value kinds; mapped to object  
  _(ItemProduct|FluidProduct)_
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::raise_player_crafted_item::recipe` - concept 'RecipeID' accepts several incompatible value kinds; mapped to object  
  _(LuaRecipePrototype|LuaRecipe|string)_
* [runtime] `LuaBootstrap::raise_script_revive::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaBootstrap::raise_script_revive::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaBurner::currently_burning::name` - concept 'ItemID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemPrototype|LuaItemStack|LuaItem|string)_
* [runtime] `LuaBurner::currently_burning::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaCommandable::command::destination` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::distraction` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::destination` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::distraction` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::distraction` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::radius` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::distraction` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::distraction` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::distraction` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::distraction` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::ticks_to_wait` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::radius` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaControl::begin_crafting::recipe` - concept 'RecipeID' accepts several incompatible value kinds; mapped to object  
  _(LuaRecipePrototype|LuaRecipe|string)_
* [runtime] `LuaControl::can_insert::items::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaControl::can_insert::items::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaControl::can_insert::items::custom_description` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaControl::can_insert::items::custom_description` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaControl::can_place_entity::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaControl::get_item_count::item::name` - concept 'ItemID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemPrototype|LuaItemStack|LuaItem|string)_
* [runtime] `LuaControl::get_item_count::item::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaElectricNetwork::get_accumulators_energy::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaElectricNetwork::get_accumulators_energy::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaElectricNetwork::set_accumulators_energy::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaElectricNetwork::set_accumulators_energy::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaElectricSubNetwork::get_accumulators_energy::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaElectricSubNetwork::get_accumulators_energy::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaElectricSubNetwork::set_accumulators_energy::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaElectricSubNetwork::set_accumulators_energy::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntity::cargo_pod_destination::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaEntity::custom_status::label` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaEntity::custom_status::label` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaEntity::infinity_container_filters::name` - concept 'ItemID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemPrototype|LuaItemStack|LuaItem|string)_
* [runtime] `LuaEntity::infinity_container_filters::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntity::previous_recipe::name` - concept 'RecipeID' accepts several incompatible value kinds; mapped to object  
  _(LuaRecipePrototype|LuaRecipe|string)_
* [runtime] `LuaEntity::previous_recipe::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntity::apply_upgrade::override_target::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaEntity::apply_upgrade::override_target::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntity::clone::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaEntity::create_cargo_pod::cargo_pod_prototype` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaEntity::destroy::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaEntity::flip::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaEntity::get_stored_durability::return::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntity::get_tooltip_field::return::name` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaEntity::get_tooltip_field::return::name` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaEntity::get_tooltip_field::return::value` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaEntity::get_tooltip_field::return::value` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaEntity::order_upgrade::target` - concept 'EntityWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|LuaEntityPrototype|string|EntityIDAndQualityIDPair)_
* [runtime] `LuaEntity::order_upgrade::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaEntity::order_upgrade::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaEntity::rotate::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaEntityPrototype::attack_parameters::projectile_creation_parameters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaEntityPrototype::hatch_definitions::cargo_unit_entity_to_spawn` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaEntityPrototype::hatch_definitions::receiving_cargo_units` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaEntityPrototype::loot::quality_min` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntityPrototype::loot::quality_max` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEquipmentGrid::count::equipment::name` - concept 'EquipmentID' accepts several incompatible value kinds; mapped to object  
  _(LuaEquipmentPrototype|LuaEquipment|string)_
* [runtime] `LuaEquipmentGrid::count::equipment::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEquipmentGrid::put::name` - concept 'EquipmentID' accepts several incompatible value kinds; mapped to object  
  _(LuaEquipmentPrototype|LuaEquipment|string)_
* [runtime] `LuaEquipmentGrid::put::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEquipmentGrid::put::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaEquipmentGrid::take::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaFlowStatistics::get_flow_count::name` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaFlowStatistics::get_flow_count::name` - concept 'FluidID' accepts several incompatible value kinds; mapped to object  
  _(string|LuaFluidPrototype|Fluid)_
* [runtime] `LuaFlowStatistics::get_flow_count::name` - concept 'EntityWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|LuaEntityPrototype|string|EntityIDAndQualityIDPair)_
* [runtime] `LuaFlowStatistics::get_flow_count::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaForce::add_chart_tag::tag::last_user` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaForce::create_space_platform::planet` - concept 'SpaceLocationID' accepts several incompatible value kinds; mapped to object  
  _(LuaSpaceLocationPrototype|string)_
* [runtime] `LuaForce::create_space_platform::starter_pack` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaForce::remove_alert::filter::prototype` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaForce::remove_alert::filter::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaForce::remove_alert::filter::message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaForce::remove_alert::filter::message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::default_map_gen_settings::starting_area` - concept 'MapGenSize' accepts several incompatible value kinds; mapped to object  
  _(float|literal(string:none)|literal(string:very-low)|literal(string:very-small)|literal(string:very-poor)|literal(string:low)|literal(string:small)|literal(string:poor)|literal(string:normal)|literal(string:medium)|literal(string:regular)|literal(string:high)|literal(string:big)|literal(string:good)|literal(string:very-high)|literal(string:very-big)|literal(string:very-good))_
* [runtime] `LuaGameScript::get_vehicles::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaGameScript::get_vehicles::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaGameScript::get_vehicles::type` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaGameScript::get_vehicles::type` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaGameScript::set_lose_ending_info::title` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::set_lose_ending_info::title` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::set_lose_ending_info::message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::set_lose_ending_info::message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::set_lose_ending_info::bullet_points` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::set_lose_ending_info::bullet_points` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::set_lose_ending_info::final_message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::set_lose_ending_info::final_message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::set_win_ending_info::title` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::set_win_ending_info::title` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::set_win_ending_info::message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::set_win_ending_info::message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::set_win_ending_info::bullet_points` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::set_win_ending_info::bullet_points` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::set_win_ending_info::final_message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::set_win_ending_info::final_message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::show_message_dialog::text` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::show_message_dialog::text` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::take_screenshot::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaGameScript::take_screenshot::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaGameScript::take_screenshot::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaGameScript::take_technology_screenshot::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaGameScript::take_technology_screenshot::selected_technology` - concept 'TechnologyID' accepts several incompatible value kinds; mapped to object  
  _(LuaTechnologyPrototype|LuaTechnology|string)_
* [runtime] `LuaGuiElement::elem_filters::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::elem_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::elem_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::elem_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::elem_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::elem_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::elem_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::elem_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::elem_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::empty_slot_info::caption` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::empty_slot_info::caption` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::empty_slot_info::text` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::empty_slot_info::text` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add::direction` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::state` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::sprite` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::items` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::add::items` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add::direction` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::items` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::text` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::position` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::badge_text` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::add::badge_text` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add::selected_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::surface_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::caption` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::add::caption` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::left_label_caption` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::add::left_label_caption` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add::tooltip` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::add::tooltip` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add::left_label_tooltip` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::add::left_label_tooltip` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add::zoom` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::right_label_caption` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::add::right_label_caption` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add::right_label_tooltip` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::add::right_label_tooltip` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add::icon_selector` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::mouse_button_filter` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::auto_toggle` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::toggled` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaGuiElement::add::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaItemCommon::build_blueprint::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaItemCommon::build_blueprint::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaItemCommon::build_blueprint::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaItemCommon::cancel_deconstruct_area::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaItemCommon::cancel_deconstruct_area::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaItemCommon::cancel_deconstruct_area::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaItemCommon::create_blueprint::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaItemCommon::create_blueprint::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaItemCommon::deconstruct_area::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaItemCommon::deconstruct_area::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaItemCommon::deconstruct_area::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::result-inventory` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::stock_connections` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::player_description` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::player_description` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::priority-list` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::priority-list` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::stock_connections` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::stock_connections` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::stock_connections` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filter` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::rail_layer` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::recipe` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::player_description` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::bar` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filter` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::priority-list` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::artillery_auto_targeting` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::bar` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::ignore-unprioritised` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::ignore-unprioritised` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::bar` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filter_mode` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filter_mode` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::bar` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::recipe_quality` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::input_priority` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::ignore-unprioritised` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::output_priority` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::infinity_settings` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::belt_stack_size_override` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::trunk_inventory` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::ammo_inventory` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::copy_color_from_train_stop` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::copy_color_from_train_stop` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::driver_is_main_gunner` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::copy_color_from_train_stop` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::inventory` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::copy_color_from_train_stop` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaItemCommon::get_blueprint_entities::return::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaItemCommon::get_blueprint_entities::return::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::infinity_settings` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::selected_gun_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemPrototype::capsule_action::attack_parameters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemPrototype::capsule_action::attack_parameters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaLogisticNetwork::select_drop_point::stack` - concept 'ItemStackIdentification' accepts several incompatible value kinds; mapped to object  
  _(string|ItemStackDefinition|LuaItemStack)_
* [runtime] `LuaLogisticNetwork::select_pickup_point::name` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaLogisticSection::filters::import_from` - concept 'SpaceLocationID' accepts several incompatible value kinds; mapped to object  
  _(LuaSpaceLocationPrototype|string)_
* [runtime] `LuaPlayer::add_pin::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaPlayer::add_pin::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaPlayer::add_pin::resource` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaPlayer::connect_to_server::name` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPlayer::connect_to_server::name` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPlayer::connect_to_server::description` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPlayer::connect_to_server::description` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPlayer::create_local_flying_text::text` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPlayer::create_local_flying_text::text` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPlayer::create_local_flying_text::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaPlayer::get_alerts::return::message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPlayer::get_alerts::return::message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPlayer::set_controller::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaPrototypeBase::custom_tooltip_fields::name` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPrototypeBase::custom_tooltip_fields::name` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPrototypeBase::custom_tooltip_fields::value` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPrototypeBase::custom_tooltip_fields::value` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPrototypeBase::custom_tooltip_fields::quality_values` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPrototypeBase::custom_tooltip_fields::quality_values` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPrototypes::utility_constants::factoriopedia_recycling_recipe_categories` - concept 'RecipeCategoryID' accepts several incompatible value kinds; mapped to object  
  _(LuaRecipeCategoryPrototype|string)_
* [runtime] `LuaRecipe::ingredients::quality_min` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaRecipe::ingredients::quality_max` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaRecord::build_blueprint::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRecord::build_blueprint::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRecord::build_blueprint::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRecord::cancel_deconstruct_area::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRecord::cancel_deconstruct_area::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRecord::cancel_deconstruct_area::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRecord::create_blueprint::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRecord::create_blueprint::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRecord::deconstruct_area::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRecord::deconstruct_area::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRecord::deconstruct_area::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRendering::draw_animation::target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_animation::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRendering::draw_animation::orientation_target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_animation::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_animation::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_animation::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRendering::draw_animation::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRendering::draw_arc::target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_arc::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRendering::draw_arc::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_arc::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_arc::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRendering::draw_arc::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRendering::draw_circle::target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_circle::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRendering::draw_circle::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_circle::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_circle::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRendering::draw_circle::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRendering::draw_light::target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_light::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRendering::draw_light::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_light::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_light::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRendering::draw_light::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRendering::draw_line::from` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_line::to` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_line::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRendering::draw_line::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_line::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_line::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRendering::draw_line::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRendering::draw_polygon::vertices` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_polygon::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRendering::draw_polygon::target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_polygon::orientation_target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_polygon::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_polygon::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_polygon::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRendering::draw_polygon::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRendering::draw_rectangle::left_top` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_rectangle::right_bottom` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_rectangle::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRendering::draw_rectangle::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_rectangle::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_rectangle::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRendering::draw_rectangle::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRendering::draw_sprite::target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_sprite::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRendering::draw_sprite::orientation_target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_sprite::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_sprite::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_sprite::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRendering::draw_sprite::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRendering::draw_text::text` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaRendering::draw_text::text` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaRendering::draw_text::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRendering::draw_text::target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_text::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_text::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_text::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRendering::draw_text::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaSegmentedUnit::clone::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSegmentedUnit::get_ai_state::return::last_damage_time` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSegmentedUnit::get_ai_state::return::destination` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSegmentedUnit::get_ai_state::return::destination` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSegmentedUnit::get_ai_state::return::remaining_time` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSegmentedUnit::get_ai_state::return::target` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSegmentedUnit::get_ai_state::return::destination` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSegmentedUnit::get_ai_state::return::destination` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSimulation::get_widget_position::item` - concept 'ItemID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemPrototype|LuaItemStack|LuaItem|string)_
* [runtime] `LuaSimulation::get_widget_position::item` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSimulation::get_widget_position::recipe` - concept 'RecipeID' accepts several incompatible value kinds; mapped to object  
  _(LuaRecipePrototype|LuaRecipe|string)_
* [runtime] `LuaSimulation::get_widget_position::filter` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSimulation::get_widget_position::request_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSimulation::get_widget_position::text` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaSimulation::get_widget_position::text` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaSimulation::get_widget_position::text` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSimulation::get_widget_position::text` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSimulation::get_widget_position::text` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSimulation::get_widget_position::group` - concept 'ItemGroupID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemGroup|string)_
* [runtime] `LuaSimulation::get_widget_position::text` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSimulation::get_widget_position::text` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSimulation::get_widget_position::text` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSpacePlatform::destroy_asteroid_chunks::name` - concept 'AsteroidChunkID' accepts several incompatible value kinds; mapped to object  
  _(LuaAsteroidChunkPrototype|string)_
* [runtime] `LuaSpacePlatform::destroy_asteroid_chunks::name` - concept 'AsteroidChunkID' accepts several incompatible value kinds; mapped to object  
  _(LuaAsteroidChunkPrototype|string)_
* [runtime] `LuaSpacePlatform::find_asteroid_chunks_filtered::name` - concept 'AsteroidChunkID' accepts several incompatible value kinds; mapped to object  
  _(LuaAsteroidChunkPrototype|string)_
* [runtime] `LuaSpacePlatform::find_asteroid_chunks_filtered::name` - concept 'AsteroidChunkID' accepts several incompatible value kinds; mapped to object  
  _(LuaAsteroidChunkPrototype|string)_
* [runtime] `LuaSurface::can_fast_replace::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::can_fast_replace::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::can_place_entity::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::can_place_entity::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::cancel_deconstruct_area::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::cancel_deconstruct_area::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaSurface::cancel_upgrade_area::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::cancel_upgrade_area::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaSurface::clone_area::destination_surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaSurface::clone_area::destination_force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::clone_brush::destination_surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaSurface::clone_entities::destination_surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaSurface::clone_entities::destination_force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::count_entities_filtered::filter::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::count_entities_filtered::filter::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::count_entities_filtered::filter::ghost_name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::count_entities_filtered::filter::ghost_name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::count_entities_filtered::filter::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::count_entities_filtered::filter::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::count_entities_filtered::filter::force` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaSurface::count_entities_filtered::filter::has_item_inside` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaSurface::count_tiles_filtered::filter::name` - concept 'TileID' accepts several incompatible value kinds; mapped to object  
  _(LuaTilePrototype|LuaTile|string)_
* [runtime] `LuaSurface::count_tiles_filtered::filter::name` - concept 'TileID' accepts several incompatible value kinds; mapped to object  
  _(LuaTilePrototype|LuaTile|string)_
* [runtime] `LuaSurface::count_tiles_filtered::filter::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::count_tiles_filtered::filter::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::count_tiles_filtered::filter::force` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaSurface::create_entities_from_blueprint_string::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::create_entities_from_blueprint_string::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaSurface::create_entity::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::result-inventory` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::player_description` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::player_description` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::text` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaSurface::create_entity::text` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaSurface::create_entity::priority-list` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::inner_name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::priority-list` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::filter` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::movement` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::speed` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::rail_layer` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::recipe` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::player_description` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::render_player_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::bar` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::text` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::filter` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::target_position` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::priority-list` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::bar` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::ignore-unprioritised` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaSurface::create_entity::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaSurface::create_entity::ignore-unprioritised` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::bar` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::bar` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::height` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::recipe_quality` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::input_priority` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::source_position` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::ignore-unprioritised` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::render_player_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::filter_mode` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::filter_mode` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::vertical_speed` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::variation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::output_priority` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::trunk_inventory` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::infinity_settings` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::belt_stack_size_override` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::frame_speed` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::ammo_inventory` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaSurface::create_entity::copy_color_from_train_stop` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::copy_color_from_train_stop` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::copy_color_from_train_stop` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::copy_color_from_train_stop` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::driver_is_main_gunner` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::create_entity::auto_connect` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::auto_connect` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::auto_connect` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::auto_connect` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::auto_connect` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::target` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::artillery_auto_targeting` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::inventory` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::selected_gun_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::infinity_settings` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::cause` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaSurface::create_particle::name` - concept 'ParticleID' accepts several incompatible value kinds; mapped to object  
  _(LuaParticlePrototype|string)_
* [runtime] `LuaSurface::create_segmented_unit::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::create_segmented_unit::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaSurface::create_segmented_unit::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::create_trivial_smoke::name` - concept 'TrivialSmokeID' accepts several incompatible value kinds; mapped to object  
  _(LuaTrivialSmokePrototype|string)_
* [runtime] `LuaSurface::create_unit_group::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::deconstruct_area::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::deconstruct_area::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaSurface::destroy_decoratives::name` - concept 'DecorativeID' accepts several incompatible value kinds; mapped to object  
  _(LuaDecorativePrototype|string)_
* [runtime] `LuaSurface::destroy_decoratives::name` - concept 'DecorativeID' accepts several incompatible value kinds; mapped to object  
  _(LuaDecorativePrototype|string)_
* [runtime] `LuaSurface::execute_lightning::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::find_decoratives_filtered::name` - concept 'DecorativeID' accepts several incompatible value kinds; mapped to object  
  _(LuaDecorativePrototype|string)_
* [runtime] `LuaSurface::find_decoratives_filtered::name` - concept 'DecorativeID' accepts several incompatible value kinds; mapped to object  
  _(LuaDecorativePrototype|string)_
* [runtime] `LuaSurface::find_nearest_enemy::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::find_nearest_enemy_entity_with_owner::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::find_units::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::request_path::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::set_multi_command::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::spill_inventory::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::spill_item_stack::stack` - concept 'ItemStackIdentification' accepts several incompatible value kinds; mapped to object  
  _(string|ItemStackDefinition|LuaItemStack)_
* [runtime] `LuaSurface::spill_item_stack::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::upgrade_area::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::upgrade_area::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::ammo_category` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::effect_description` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaTechnologyPrototype::effects::effect_description` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::recipe` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::quality` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::research_trigger::entity` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::research_trigger::item` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::research_trigger::trigger_description` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaTechnologyPrototype::research_trigger::trigger_description` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaTrainManager::get_train_stops::filter::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaTrainManager::get_train_stops::filter::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaTrainManager::get_train_stops::filter::type` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaTrainManager::get_train_stops::filter::type` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaTrainManager::get_trains::filter::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaTrainManager::get_trains::filter::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaTrainManager::get_trains::filter::stock` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaTrainManager::get_trains::filter::stock` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaTrainManager::request_train_path::goals` - concept 'TrainPathFinderGoal' accepts several incompatible value kinds; mapped to object  
  _(TrainStopGoal|RailEndGoal|LuaRailEnd|LuaEntity)_
* [runtime] `LuaUndoRedoStack::get_redo_item::return::target` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::position` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::target` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::target` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::a` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::target` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::target` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaUndoRedoStack::get_redo_item::return::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaUndoRedoStack::get_redo_item::return::surface_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::surface_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::surface_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::surface_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::surface_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::b` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::surface_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::surface_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::previous_tile` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `event on_blueprint_settings_pasted::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `event on_blueprint_settings_pasted::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `event on_built_entity::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `event on_built_entity::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `event on_robot_built_entity::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `event on_robot_built_entity::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `event on_space_platform_built_entity::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `event on_space_platform_built_entity::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `event on_string_translated::localised_string` - recursive concept 'LocalisedString' mapped to object
* [runtime] `event on_string_translated::localised_string` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `event script_raised_revive::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `event script_raised_revive::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaAccumulatorControlBehavior::output_signal::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::entity_name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::entity_name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::entity_name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::damage` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::radius` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offsets` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offsets` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offsets` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offsets` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offsets` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offsets` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::radius` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::radius` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offset_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offset_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offset_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offset_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offset_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offset_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::trigger_created_entity` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::trigger_created_entity` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::trigger_created_entity` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::tile_collision_mask` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::trigger_created_entity` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::check_buildability` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::check_buildability` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::initial_height` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::check_buildability` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::tile_collision_mask` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::max_distance` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::only_when_visible` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::only_when_visible` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::only_when_visible` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::apply_projection` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::tile_collision_mask` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::tile_collision_mask` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::tile_collision_mask` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::tile_collision_mask` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::as_enemy` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::as_enemy` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::as_enemy` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::ignore_no_enemies_mode` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::ignore_no_enemies_mode` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::ignore_no_enemies_mode` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::protected` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::protected` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::protected` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::find_non_colliding_position` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::find_non_colliding_position` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::find_non_colliding_position` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::speed_from_center` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::abort_if_over_space` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::abort_if_over_space` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::abort_if_over_space` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::speed_from_center_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::non_colliding_search_radius` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::non_colliding_search_radius` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::non_colliding_search_radius` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::non_colliding_search_precision` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::non_colliding_search_precision` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::non_colliding_search_precision` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::non_colliding_fail_result` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::non_colliding_fail_result` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::non_colliding_fail_result` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::initial_height` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::speed` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::only_when_visible` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::speed_multiplier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::speed_multiplier_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::starting_frame` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::starting_frame_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::speed_from_center` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::speed_from_center_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::mineable_properties::products` - concept 'Product' accepts several incompatible value kinds; mapped to object  
  _(ItemProduct|FluidProduct)_
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::raise_player_crafted_item::recipe` - concept 'RecipeID' accepts several incompatible value kinds; mapped to object  
  _(LuaRecipePrototype|LuaRecipe|string)_
* [runtime] `LuaBootstrap::raise_script_revive::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaBootstrap::raise_script_revive::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaBurner::currently_burning::name` - concept 'ItemID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemPrototype|LuaItemStack|LuaItem|string)_
* [runtime] `LuaBurner::currently_burning::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaCommandable::command::destination` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::distraction` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::destination` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::distraction` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::distraction` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::radius` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::distraction` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::distraction` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::distraction` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::distraction` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::ticks_to_wait` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::radius` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaControl::begin_crafting::recipe` - concept 'RecipeID' accepts several incompatible value kinds; mapped to object  
  _(LuaRecipePrototype|LuaRecipe|string)_
* [runtime] `LuaControl::can_insert::items::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaControl::can_insert::items::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaControl::can_insert::items::custom_description` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaControl::can_insert::items::custom_description` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaControl::can_place_entity::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaControl::get_item_count::item::name` - concept 'ItemID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemPrototype|LuaItemStack|LuaItem|string)_
* [runtime] `LuaControl::get_item_count::item::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaElectricNetwork::get_accumulators_energy::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaElectricNetwork::get_accumulators_energy::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaElectricNetwork::set_accumulators_energy::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaElectricNetwork::set_accumulators_energy::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaElectricSubNetwork::get_accumulators_energy::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaElectricSubNetwork::get_accumulators_energy::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaElectricSubNetwork::set_accumulators_energy::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaElectricSubNetwork::set_accumulators_energy::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntity::cargo_pod_destination::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaEntity::custom_status::label` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaEntity::custom_status::label` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaEntity::infinity_container_filters::name` - concept 'ItemID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemPrototype|LuaItemStack|LuaItem|string)_
* [runtime] `LuaEntity::infinity_container_filters::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntity::previous_recipe::name` - concept 'RecipeID' accepts several incompatible value kinds; mapped to object  
  _(LuaRecipePrototype|LuaRecipe|string)_
* [runtime] `LuaEntity::previous_recipe::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntity::apply_upgrade::override_target::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaEntity::apply_upgrade::override_target::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntity::clone::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaEntity::create_cargo_pod::cargo_pod_prototype` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaEntity::destroy::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaEntity::flip::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaEntity::get_stored_durability::return::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntity::get_tooltip_field::return::name` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaEntity::get_tooltip_field::return::name` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaEntity::get_tooltip_field::return::value` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaEntity::get_tooltip_field::return::value` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaEntity::order_upgrade::target` - concept 'EntityWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|LuaEntityPrototype|string|EntityIDAndQualityIDPair)_
* [runtime] `LuaEntity::order_upgrade::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaEntity::order_upgrade::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaEntity::rotate::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaEntityPrototype::attack_parameters::projectile_creation_parameters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaEntityPrototype::hatch_definitions::cargo_unit_entity_to_spawn` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaEntityPrototype::hatch_definitions::receiving_cargo_units` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaEntityPrototype::loot::quality_min` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntityPrototype::loot::quality_max` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEquipmentGrid::count::equipment::name` - concept 'EquipmentID' accepts several incompatible value kinds; mapped to object  
  _(LuaEquipmentPrototype|LuaEquipment|string)_
* [runtime] `LuaEquipmentGrid::count::equipment::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEquipmentGrid::put::name` - concept 'EquipmentID' accepts several incompatible value kinds; mapped to object  
  _(LuaEquipmentPrototype|LuaEquipment|string)_
* [runtime] `LuaEquipmentGrid::put::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEquipmentGrid::put::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaEquipmentGrid::take::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaFlowStatistics::get_flow_count::name` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaFlowStatistics::get_flow_count::name` - concept 'FluidID' accepts several incompatible value kinds; mapped to object  
  _(string|LuaFluidPrototype|Fluid)_
* [runtime] `LuaFlowStatistics::get_flow_count::name` - concept 'EntityWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|LuaEntityPrototype|string|EntityIDAndQualityIDPair)_
* [runtime] `LuaFlowStatistics::get_flow_count::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaForce::add_chart_tag::tag::last_user` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaForce::create_space_platform::planet` - concept 'SpaceLocationID' accepts several incompatible value kinds; mapped to object  
  _(LuaSpaceLocationPrototype|string)_
* [runtime] `LuaForce::create_space_platform::starter_pack` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaForce::remove_alert::filter::prototype` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaForce::remove_alert::filter::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaForce::remove_alert::filter::message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaForce::remove_alert::filter::message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::default_map_gen_settings::starting_area` - concept 'MapGenSize' accepts several incompatible value kinds; mapped to object  
  _(float|literal(string:none)|literal(string:very-low)|literal(string:very-small)|literal(string:very-poor)|literal(string:low)|literal(string:small)|literal(string:poor)|literal(string:normal)|literal(string:medium)|literal(string:regular)|literal(string:high)|literal(string:big)|literal(string:good)|literal(string:very-high)|literal(string:very-big)|literal(string:very-good))_
* [runtime] `LuaGameScript::get_vehicles::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaGameScript::get_vehicles::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaGameScript::get_vehicles::type` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaGameScript::get_vehicles::type` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaGameScript::set_lose_ending_info::title` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::set_lose_ending_info::title` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::set_lose_ending_info::message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::set_lose_ending_info::message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::set_lose_ending_info::bullet_points` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::set_lose_ending_info::bullet_points` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::set_lose_ending_info::final_message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::set_lose_ending_info::final_message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::set_win_ending_info::title` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::set_win_ending_info::title` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::set_win_ending_info::message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::set_win_ending_info::message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::set_win_ending_info::bullet_points` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::set_win_ending_info::bullet_points` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::set_win_ending_info::final_message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::set_win_ending_info::final_message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::show_message_dialog::text` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::show_message_dialog::text` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::take_screenshot::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaGameScript::take_screenshot::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaGameScript::take_screenshot::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaGameScript::take_technology_screenshot::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaGameScript::take_technology_screenshot::selected_technology` - concept 'TechnologyID' accepts several incompatible value kinds; mapped to object  
  _(LuaTechnologyPrototype|LuaTechnology|string)_
* [runtime] `LuaGuiElement::elem_filters::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::elem_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::elem_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::elem_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::elem_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::elem_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::elem_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::elem_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::elem_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::empty_slot_info::caption` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::empty_slot_info::caption` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::empty_slot_info::text` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::empty_slot_info::text` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add::direction` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::state` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::sprite` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::items` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::add::items` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add::direction` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::items` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::text` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::position` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::badge_text` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::add::badge_text` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add::selected_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::surface_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::caption` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::add::caption` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::left_label_caption` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::add::left_label_caption` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add::tooltip` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::add::tooltip` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add::left_label_tooltip` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::add::left_label_tooltip` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add::zoom` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::right_label_caption` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::add::right_label_caption` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add::right_label_tooltip` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::add::right_label_tooltip` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add::icon_selector` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::mouse_button_filter` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::auto_toggle` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::toggled` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaGuiElement::add::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaItemCommon::build_blueprint::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaItemCommon::build_blueprint::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaItemCommon::build_blueprint::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaItemCommon::cancel_deconstruct_area::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaItemCommon::cancel_deconstruct_area::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaItemCommon::cancel_deconstruct_area::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaItemCommon::create_blueprint::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaItemCommon::create_blueprint::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaItemCommon::deconstruct_area::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaItemCommon::deconstruct_area::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaItemCommon::deconstruct_area::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::result-inventory` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::stock_connections` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::player_description` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::player_description` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::priority-list` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::priority-list` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::stock_connections` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::stock_connections` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::stock_connections` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filter` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::rail_layer` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::recipe` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::player_description` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::bar` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filter` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::priority-list` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::artillery_auto_targeting` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::bar` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::ignore-unprioritised` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::ignore-unprioritised` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::bar` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filter_mode` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filter_mode` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::bar` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::recipe_quality` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::input_priority` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::ignore-unprioritised` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::output_priority` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::infinity_settings` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::belt_stack_size_override` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::trunk_inventory` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::ammo_inventory` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::copy_color_from_train_stop` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::copy_color_from_train_stop` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::driver_is_main_gunner` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::copy_color_from_train_stop` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::inventory` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::copy_color_from_train_stop` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaItemCommon::get_blueprint_entities::return::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaItemCommon::get_blueprint_entities::return::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::infinity_settings` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::selected_gun_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemPrototype::capsule_action::attack_parameters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemPrototype::capsule_action::attack_parameters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaLogisticNetwork::select_drop_point::stack` - concept 'ItemStackIdentification' accepts several incompatible value kinds; mapped to object  
  _(string|ItemStackDefinition|LuaItemStack)_
* [runtime] `LuaLogisticNetwork::select_pickup_point::name` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaLogisticSection::filters::import_from` - concept 'SpaceLocationID' accepts several incompatible value kinds; mapped to object  
  _(LuaSpaceLocationPrototype|string)_
* [runtime] `LuaPlayer::add_pin::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaPlayer::add_pin::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaPlayer::add_pin::resource` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaPlayer::connect_to_server::name` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPlayer::connect_to_server::name` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPlayer::connect_to_server::description` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPlayer::connect_to_server::description` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPlayer::create_local_flying_text::text` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPlayer::create_local_flying_text::text` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPlayer::create_local_flying_text::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaPlayer::get_alerts::return::message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPlayer::get_alerts::return::message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPlayer::set_controller::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaPrototypeBase::custom_tooltip_fields::name` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPrototypeBase::custom_tooltip_fields::name` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPrototypeBase::custom_tooltip_fields::value` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPrototypeBase::custom_tooltip_fields::value` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPrototypeBase::custom_tooltip_fields::quality_values` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPrototypeBase::custom_tooltip_fields::quality_values` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPrototypes::utility_constants::factoriopedia_recycling_recipe_categories` - concept 'RecipeCategoryID' accepts several incompatible value kinds; mapped to object  
  _(LuaRecipeCategoryPrototype|string)_
* [runtime] `LuaRecipe::ingredients::quality_min` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaRecipe::ingredients::quality_max` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaRecord::build_blueprint::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRecord::build_blueprint::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRecord::build_blueprint::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRecord::cancel_deconstruct_area::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRecord::cancel_deconstruct_area::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRecord::cancel_deconstruct_area::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRecord::create_blueprint::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRecord::create_blueprint::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRecord::deconstruct_area::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRecord::deconstruct_area::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRecord::deconstruct_area::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRendering::draw_animation::target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_animation::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRendering::draw_animation::orientation_target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_animation::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_animation::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_animation::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRendering::draw_animation::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRendering::draw_arc::target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_arc::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRendering::draw_arc::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_arc::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_arc::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRendering::draw_arc::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRendering::draw_circle::target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_circle::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRendering::draw_circle::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_circle::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_circle::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRendering::draw_circle::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRendering::draw_light::target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_light::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRendering::draw_light::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_light::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_light::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRendering::draw_light::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRendering::draw_line::from` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_line::to` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_line::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRendering::draw_line::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_line::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_line::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRendering::draw_line::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRendering::draw_polygon::vertices` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_polygon::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRendering::draw_polygon::target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_polygon::orientation_target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_polygon::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_polygon::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_polygon::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRendering::draw_polygon::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRendering::draw_rectangle::left_top` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_rectangle::right_bottom` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_rectangle::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRendering::draw_rectangle::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_rectangle::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_rectangle::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRendering::draw_rectangle::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRendering::draw_sprite::target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_sprite::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRendering::draw_sprite::orientation_target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_sprite::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_sprite::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_sprite::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRendering::draw_sprite::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRendering::draw_text::text` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaRendering::draw_text::text` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaRendering::draw_text::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRendering::draw_text::target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_text::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_text::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_text::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRendering::draw_text::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaSegmentedUnit::clone::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSegmentedUnit::get_ai_state::return::last_damage_time` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSegmentedUnit::get_ai_state::return::destination` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSegmentedUnit::get_ai_state::return::destination` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSegmentedUnit::get_ai_state::return::remaining_time` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSegmentedUnit::get_ai_state::return::target` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSegmentedUnit::get_ai_state::return::destination` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSegmentedUnit::get_ai_state::return::destination` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSimulation::get_widget_position::item` - concept 'ItemID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemPrototype|LuaItemStack|LuaItem|string)_
* [runtime] `LuaSimulation::get_widget_position::item` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSimulation::get_widget_position::recipe` - concept 'RecipeID' accepts several incompatible value kinds; mapped to object  
  _(LuaRecipePrototype|LuaRecipe|string)_
* [runtime] `LuaSimulation::get_widget_position::filter` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSimulation::get_widget_position::request_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSimulation::get_widget_position::text` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaSimulation::get_widget_position::text` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaSimulation::get_widget_position::text` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSimulation::get_widget_position::text` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSimulation::get_widget_position::text` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSimulation::get_widget_position::group` - concept 'ItemGroupID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemGroup|string)_
* [runtime] `LuaSimulation::get_widget_position::text` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSimulation::get_widget_position::text` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSimulation::get_widget_position::text` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSpacePlatform::destroy_asteroid_chunks::name` - concept 'AsteroidChunkID' accepts several incompatible value kinds; mapped to object  
  _(LuaAsteroidChunkPrototype|string)_
* [runtime] `LuaSpacePlatform::destroy_asteroid_chunks::name` - concept 'AsteroidChunkID' accepts several incompatible value kinds; mapped to object  
  _(LuaAsteroidChunkPrototype|string)_
* [runtime] `LuaSpacePlatform::find_asteroid_chunks_filtered::name` - concept 'AsteroidChunkID' accepts several incompatible value kinds; mapped to object  
  _(LuaAsteroidChunkPrototype|string)_
* [runtime] `LuaSpacePlatform::find_asteroid_chunks_filtered::name` - concept 'AsteroidChunkID' accepts several incompatible value kinds; mapped to object  
  _(LuaAsteroidChunkPrototype|string)_
* [runtime] `LuaSurface::can_fast_replace::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::can_fast_replace::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::can_place_entity::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::can_place_entity::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::cancel_deconstruct_area::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::cancel_deconstruct_area::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaSurface::cancel_upgrade_area::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::cancel_upgrade_area::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaSurface::clone_area::destination_surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaSurface::clone_area::destination_force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::clone_brush::destination_surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaSurface::clone_entities::destination_surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaSurface::clone_entities::destination_force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::count_entities_filtered::filter::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::count_entities_filtered::filter::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::count_entities_filtered::filter::ghost_name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::count_entities_filtered::filter::ghost_name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::count_entities_filtered::filter::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::count_entities_filtered::filter::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::count_entities_filtered::filter::force` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaSurface::count_entities_filtered::filter::has_item_inside` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaSurface::count_tiles_filtered::filter::name` - concept 'TileID' accepts several incompatible value kinds; mapped to object  
  _(LuaTilePrototype|LuaTile|string)_
* [runtime] `LuaSurface::count_tiles_filtered::filter::name` - concept 'TileID' accepts several incompatible value kinds; mapped to object  
  _(LuaTilePrototype|LuaTile|string)_
* [runtime] `LuaSurface::count_tiles_filtered::filter::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::count_tiles_filtered::filter::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::count_tiles_filtered::filter::force` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaSurface::create_entities_from_blueprint_string::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::create_entities_from_blueprint_string::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaSurface::create_entity::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::result-inventory` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::player_description` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::player_description` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::text` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaSurface::create_entity::text` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaSurface::create_entity::priority-list` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::inner_name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::priority-list` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::filter` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::movement` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::speed` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::rail_layer` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::recipe` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::player_description` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::render_player_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::bar` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::text` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::filter` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::target_position` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::priority-list` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::bar` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::ignore-unprioritised` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaSurface::create_entity::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaSurface::create_entity::ignore-unprioritised` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::bar` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::bar` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::height` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::recipe_quality` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::input_priority` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::source_position` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::ignore-unprioritised` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::render_player_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::filter_mode` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::filter_mode` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::vertical_speed` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::variation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::output_priority` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::trunk_inventory` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::infinity_settings` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::belt_stack_size_override` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::frame_speed` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::ammo_inventory` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaSurface::create_entity::copy_color_from_train_stop` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::copy_color_from_train_stop` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::copy_color_from_train_stop` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::copy_color_from_train_stop` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::driver_is_main_gunner` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::create_entity::auto_connect` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::auto_connect` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::auto_connect` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::auto_connect` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::auto_connect` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::target` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::artillery_auto_targeting` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::inventory` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::selected_gun_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::infinity_settings` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::cause` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaSurface::create_particle::name` - concept 'ParticleID' accepts several incompatible value kinds; mapped to object  
  _(LuaParticlePrototype|string)_
* [runtime] `LuaSurface::create_segmented_unit::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::create_segmented_unit::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaSurface::create_segmented_unit::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::create_trivial_smoke::name` - concept 'TrivialSmokeID' accepts several incompatible value kinds; mapped to object  
  _(LuaTrivialSmokePrototype|string)_
* [runtime] `LuaSurface::create_unit_group::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::deconstruct_area::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::deconstruct_area::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaSurface::destroy_decoratives::name` - concept 'DecorativeID' accepts several incompatible value kinds; mapped to object  
  _(LuaDecorativePrototype|string)_
* [runtime] `LuaSurface::destroy_decoratives::name` - concept 'DecorativeID' accepts several incompatible value kinds; mapped to object  
  _(LuaDecorativePrototype|string)_
* [runtime] `LuaSurface::execute_lightning::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::find_decoratives_filtered::name` - concept 'DecorativeID' accepts several incompatible value kinds; mapped to object  
  _(LuaDecorativePrototype|string)_
* [runtime] `LuaSurface::find_decoratives_filtered::name` - concept 'DecorativeID' accepts several incompatible value kinds; mapped to object  
  _(LuaDecorativePrototype|string)_
* [runtime] `LuaSurface::find_nearest_enemy::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::find_nearest_enemy_entity_with_owner::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::find_units::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::request_path::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::set_multi_command::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::spill_inventory::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::spill_item_stack::stack` - concept 'ItemStackIdentification' accepts several incompatible value kinds; mapped to object  
  _(string|ItemStackDefinition|LuaItemStack)_
* [runtime] `LuaSurface::spill_item_stack::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::upgrade_area::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::upgrade_area::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::ammo_category` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::effect_description` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaTechnologyPrototype::effects::effect_description` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::recipe` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::quality` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::research_trigger::entity` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::research_trigger::item` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::research_trigger::trigger_description` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaTechnologyPrototype::research_trigger::trigger_description` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaTrainManager::get_train_stops::filter::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaTrainManager::get_train_stops::filter::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaTrainManager::get_train_stops::filter::type` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaTrainManager::get_train_stops::filter::type` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaTrainManager::get_trains::filter::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaTrainManager::get_trains::filter::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaTrainManager::get_trains::filter::stock` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaTrainManager::get_trains::filter::stock` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaTrainManager::request_train_path::goals` - concept 'TrainPathFinderGoal' accepts several incompatible value kinds; mapped to object  
  _(TrainStopGoal|RailEndGoal|LuaRailEnd|LuaEntity)_
* [runtime] `LuaUndoRedoStack::get_redo_item::return::target` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::position` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::target` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::target` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::a` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::target` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::target` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaUndoRedoStack::get_redo_item::return::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaUndoRedoStack::get_redo_item::return::surface_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::surface_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::surface_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::surface_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::surface_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::b` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::surface_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::surface_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::previous_tile` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `event on_blueprint_settings_pasted::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `event on_blueprint_settings_pasted::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `event on_built_entity::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `event on_built_entity::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `event on_robot_built_entity::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `event on_robot_built_entity::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `event on_space_platform_built_entity::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `event on_space_platform_built_entity::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `event on_string_translated::localised_string` - recursive concept 'LocalisedString' mapped to object
* [runtime] `event on_string_translated::localised_string` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `event script_raised_revive::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `event script_raised_revive::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaEntityPrototype::action::action_delivery::projectile` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaEntityPrototype::action::action_delivery::starting_speed` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaEntityPrototype::action::action_delivery::starting_speed_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaEntityPrototype::action::action_delivery::direction_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaEntityPrototype::action::action_delivery::range_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaEntityPrototype::action::action_delivery::source_offset` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGameScript::default_map_gen_settings::autoplace_controls::frequency` - concept 'MapGenSize' accepts several incompatible value kinds; mapped to object  
  _(float|literal(string:none)|literal(string:very-low)|literal(string:very-small)|literal(string:very-poor)|literal(string:low)|literal(string:small)|literal(string:poor)|literal(string:normal)|literal(string:medium)|literal(string:regular)|literal(string:high)|literal(string:big)|literal(string:good)|literal(string:very-high)|literal(string:very-big)|literal(string:very-good))_
* [runtime] `LuaGameScript::default_map_gen_settings::autoplace_controls::size` - concept 'MapGenSize' accepts several incompatible value kinds; mapped to object  
  _(float|literal(string:none)|literal(string:very-low)|literal(string:very-small)|literal(string:very-poor)|literal(string:low)|literal(string:small)|literal(string:poor)|literal(string:normal)|literal(string:medium)|literal(string:regular)|literal(string:high)|literal(string:big)|literal(string:good)|literal(string:very-high)|literal(string:very-big)|literal(string:very-good))_
* [runtime] `LuaGameScript::default_map_gen_settings::autoplace_controls::richness` - concept 'MapGenSize' accepts several incompatible value kinds; mapped to object  
  _(float|literal(string:none)|literal(string:very-low)|literal(string:very-small)|literal(string:very-poor)|literal(string:low)|literal(string:small)|literal(string:poor)|literal(string:normal)|literal(string:medium)|literal(string:regular)|literal(string:high)|literal(string:big)|literal(string:good)|literal(string:very-high)|literal(string:very-big)|literal(string:very-good))_
* [runtime] `LuaGameScript::default_map_gen_settings::cliff_settings::richness` - concept 'MapGenSize' accepts several incompatible value kinds; mapped to object  
  _(float|literal(string:none)|literal(string:very-low)|literal(string:very-small)|literal(string:very-poor)|literal(string:low)|literal(string:small)|literal(string:poor)|literal(string:normal)|literal(string:medium)|literal(string:regular)|literal(string:high)|literal(string:big)|literal(string:good)|literal(string:very-high)|literal(string:very-big)|literal(string:very-good))_
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filters::name` - concept 'ItemID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemPrototype|LuaItemStack|LuaItem|string)_
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filters::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaItemCommon::get_blueprint_entities::return::grid::equipment` - concept 'EquipmentWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEquipmentPrototype|LuaEquipment|string|EquipmentIDAndQualityIDPair)_
* [runtime] `LuaLogisticSection::filters::value::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaPrototypes::utility_constants::main_menu_simulations::planet` - concept 'SpaceLocationID' accepts several incompatible value kinds; mapped to object  
  _(LuaSpaceLocationPrototype|string)_
* [runtime] `LuaPrototypes::utility_constants::color_filters::localised_name` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPrototypes::utility_constants::color_filters::localised_name` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaUndoRedoStack::get_redo_item::return::grid::id` - concept 'EquipmentWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEquipmentPrototype|LuaEquipment|string|EquipmentIDAndQualityIDPair)_
* [runtime] `LuaAccumulatorControlBehavior::output_signal::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::entity_name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::entity_name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::entity_name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::damage` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::radius` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offsets` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offsets` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offsets` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offsets` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offsets` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offsets` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::radius` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::radius` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offset_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offset_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offset_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offset_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offset_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offset_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::trigger_created_entity` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::trigger_created_entity` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::trigger_created_entity` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::tile_collision_mask` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::trigger_created_entity` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::check_buildability` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::check_buildability` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::initial_height` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::check_buildability` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::tile_collision_mask` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::max_distance` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::only_when_visible` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::only_when_visible` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::only_when_visible` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::apply_projection` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::tile_collision_mask` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::tile_collision_mask` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::tile_collision_mask` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::tile_collision_mask` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::as_enemy` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::as_enemy` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::as_enemy` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::ignore_no_enemies_mode` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::ignore_no_enemies_mode` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::ignore_no_enemies_mode` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::protected` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::protected` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::protected` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::find_non_colliding_position` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::find_non_colliding_position` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::find_non_colliding_position` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::speed_from_center` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::abort_if_over_space` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::abort_if_over_space` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::abort_if_over_space` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::speed_from_center_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::non_colliding_search_radius` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::non_colliding_search_radius` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::non_colliding_search_radius` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::non_colliding_search_precision` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::non_colliding_search_precision` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::non_colliding_search_precision` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::non_colliding_fail_result` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::non_colliding_fail_result` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::non_colliding_fail_result` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::initial_height` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::speed` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::only_when_visible` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::speed_multiplier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::speed_multiplier_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::starting_frame` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::starting_frame_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::speed_from_center` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::speed_from_center_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::mineable_properties::products` - concept 'Product' accepts several incompatible value kinds; mapped to object  
  _(ItemProduct|FluidProduct)_
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::raise_player_crafted_item::recipe` - concept 'RecipeID' accepts several incompatible value kinds; mapped to object  
  _(LuaRecipePrototype|LuaRecipe|string)_
* [runtime] `LuaBootstrap::raise_script_revive::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaBootstrap::raise_script_revive::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaBurner::currently_burning::name` - concept 'ItemID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemPrototype|LuaItemStack|LuaItem|string)_
* [runtime] `LuaBurner::currently_burning::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaCommandable::command::destination` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::distraction` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::destination` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::distraction` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::distraction` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::radius` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::distraction` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::distraction` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::distraction` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::distraction` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::ticks_to_wait` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::radius` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaControl::begin_crafting::recipe` - concept 'RecipeID' accepts several incompatible value kinds; mapped to object  
  _(LuaRecipePrototype|LuaRecipe|string)_
* [runtime] `LuaControl::can_insert::items::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaControl::can_insert::items::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaControl::can_insert::items::custom_description` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaControl::can_insert::items::custom_description` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaControl::can_place_entity::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaControl::get_item_count::item::name` - concept 'ItemID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemPrototype|LuaItemStack|LuaItem|string)_
* [runtime] `LuaControl::get_item_count::item::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaElectricNetwork::get_accumulators_energy::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaElectricNetwork::get_accumulators_energy::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaElectricNetwork::set_accumulators_energy::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaElectricNetwork::set_accumulators_energy::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaElectricSubNetwork::get_accumulators_energy::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaElectricSubNetwork::get_accumulators_energy::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaElectricSubNetwork::set_accumulators_energy::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaElectricSubNetwork::set_accumulators_energy::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntity::cargo_pod_destination::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaEntity::custom_status::label` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaEntity::custom_status::label` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaEntity::infinity_container_filters::name` - concept 'ItemID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemPrototype|LuaItemStack|LuaItem|string)_
* [runtime] `LuaEntity::infinity_container_filters::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntity::previous_recipe::name` - concept 'RecipeID' accepts several incompatible value kinds; mapped to object  
  _(LuaRecipePrototype|LuaRecipe|string)_
* [runtime] `LuaEntity::previous_recipe::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntity::apply_upgrade::override_target::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaEntity::apply_upgrade::override_target::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntity::clone::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaEntity::create_cargo_pod::cargo_pod_prototype` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaEntity::destroy::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaEntity::flip::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaEntity::get_stored_durability::return::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntity::get_tooltip_field::return::name` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaEntity::get_tooltip_field::return::name` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaEntity::get_tooltip_field::return::value` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaEntity::get_tooltip_field::return::value` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaEntity::order_upgrade::target` - concept 'EntityWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|LuaEntityPrototype|string|EntityIDAndQualityIDPair)_
* [runtime] `LuaEntity::order_upgrade::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaEntity::order_upgrade::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaEntity::rotate::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaEntityPrototype::attack_parameters::projectile_creation_parameters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaEntityPrototype::hatch_definitions::cargo_unit_entity_to_spawn` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaEntityPrototype::hatch_definitions::receiving_cargo_units` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaEntityPrototype::loot::quality_min` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntityPrototype::loot::quality_max` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEquipmentGrid::count::equipment::name` - concept 'EquipmentID' accepts several incompatible value kinds; mapped to object  
  _(LuaEquipmentPrototype|LuaEquipment|string)_
* [runtime] `LuaEquipmentGrid::count::equipment::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEquipmentGrid::put::name` - concept 'EquipmentID' accepts several incompatible value kinds; mapped to object  
  _(LuaEquipmentPrototype|LuaEquipment|string)_
* [runtime] `LuaEquipmentGrid::put::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEquipmentGrid::put::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaEquipmentGrid::take::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaFlowStatistics::get_flow_count::name` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaFlowStatistics::get_flow_count::name` - concept 'FluidID' accepts several incompatible value kinds; mapped to object  
  _(string|LuaFluidPrototype|Fluid)_
* [runtime] `LuaFlowStatistics::get_flow_count::name` - concept 'EntityWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|LuaEntityPrototype|string|EntityIDAndQualityIDPair)_
* [runtime] `LuaFlowStatistics::get_flow_count::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaForce::add_chart_tag::tag::last_user` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaForce::create_space_platform::planet` - concept 'SpaceLocationID' accepts several incompatible value kinds; mapped to object  
  _(LuaSpaceLocationPrototype|string)_
* [runtime] `LuaForce::create_space_platform::starter_pack` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaForce::remove_alert::filter::prototype` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaForce::remove_alert::filter::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaForce::remove_alert::filter::message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaForce::remove_alert::filter::message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::default_map_gen_settings::starting_area` - concept 'MapGenSize' accepts several incompatible value kinds; mapped to object  
  _(float|literal(string:none)|literal(string:very-low)|literal(string:very-small)|literal(string:very-poor)|literal(string:low)|literal(string:small)|literal(string:poor)|literal(string:normal)|literal(string:medium)|literal(string:regular)|literal(string:high)|literal(string:big)|literal(string:good)|literal(string:very-high)|literal(string:very-big)|literal(string:very-good))_
* [runtime] `LuaGameScript::get_vehicles::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaGameScript::get_vehicles::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaGameScript::get_vehicles::type` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaGameScript::get_vehicles::type` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaGameScript::set_lose_ending_info::title` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::set_lose_ending_info::title` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::set_lose_ending_info::message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::set_lose_ending_info::message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::set_lose_ending_info::bullet_points` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::set_lose_ending_info::bullet_points` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::set_lose_ending_info::final_message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::set_lose_ending_info::final_message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::set_win_ending_info::title` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::set_win_ending_info::title` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::set_win_ending_info::message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::set_win_ending_info::message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::set_win_ending_info::bullet_points` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::set_win_ending_info::bullet_points` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::set_win_ending_info::final_message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::set_win_ending_info::final_message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::show_message_dialog::text` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::show_message_dialog::text` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::take_screenshot::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaGameScript::take_screenshot::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaGameScript::take_screenshot::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaGameScript::take_technology_screenshot::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaGameScript::take_technology_screenshot::selected_technology` - concept 'TechnologyID' accepts several incompatible value kinds; mapped to object  
  _(LuaTechnologyPrototype|LuaTechnology|string)_
* [runtime] `LuaGuiElement::elem_filters::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::elem_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::elem_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::elem_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::elem_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::elem_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::elem_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::elem_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::elem_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::empty_slot_info::caption` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::empty_slot_info::caption` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::empty_slot_info::text` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::empty_slot_info::text` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add::direction` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::state` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::sprite` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::items` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::add::items` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add::direction` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::items` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::text` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::position` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::badge_text` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::add::badge_text` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add::selected_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::surface_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::caption` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::add::caption` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::left_label_caption` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::add::left_label_caption` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add::tooltip` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::add::tooltip` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add::left_label_tooltip` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::add::left_label_tooltip` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add::zoom` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::right_label_caption` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::add::right_label_caption` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add::right_label_tooltip` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::add::right_label_tooltip` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add::icon_selector` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::mouse_button_filter` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::auto_toggle` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::toggled` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaGuiElement::add::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaItemCommon::build_blueprint::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaItemCommon::build_blueprint::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaItemCommon::build_blueprint::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaItemCommon::cancel_deconstruct_area::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaItemCommon::cancel_deconstruct_area::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaItemCommon::cancel_deconstruct_area::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaItemCommon::create_blueprint::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaItemCommon::create_blueprint::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaItemCommon::deconstruct_area::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaItemCommon::deconstruct_area::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaItemCommon::deconstruct_area::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::result-inventory` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::stock_connections` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::player_description` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::player_description` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::priority-list` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::priority-list` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::stock_connections` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::stock_connections` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::stock_connections` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filter` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::rail_layer` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::recipe` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::player_description` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::bar` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filter` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::priority-list` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::artillery_auto_targeting` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::bar` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::ignore-unprioritised` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::ignore-unprioritised` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::bar` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filter_mode` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filter_mode` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::bar` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::recipe_quality` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::input_priority` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::ignore-unprioritised` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::output_priority` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::infinity_settings` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::belt_stack_size_override` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::trunk_inventory` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::ammo_inventory` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::copy_color_from_train_stop` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::copy_color_from_train_stop` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::driver_is_main_gunner` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::copy_color_from_train_stop` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::inventory` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::copy_color_from_train_stop` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaItemCommon::get_blueprint_entities::return::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaItemCommon::get_blueprint_entities::return::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::infinity_settings` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::selected_gun_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemPrototype::capsule_action::attack_parameters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemPrototype::capsule_action::attack_parameters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaLogisticNetwork::select_drop_point::stack` - concept 'ItemStackIdentification' accepts several incompatible value kinds; mapped to object  
  _(string|ItemStackDefinition|LuaItemStack)_
* [runtime] `LuaLogisticNetwork::select_pickup_point::name` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaLogisticSection::filters::import_from` - concept 'SpaceLocationID' accepts several incompatible value kinds; mapped to object  
  _(LuaSpaceLocationPrototype|string)_
* [runtime] `LuaPlayer::add_pin::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaPlayer::add_pin::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaPlayer::add_pin::resource` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaPlayer::connect_to_server::name` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPlayer::connect_to_server::name` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPlayer::connect_to_server::description` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPlayer::connect_to_server::description` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPlayer::create_local_flying_text::text` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPlayer::create_local_flying_text::text` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPlayer::create_local_flying_text::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaPlayer::get_alerts::return::message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPlayer::get_alerts::return::message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPlayer::set_controller::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaPrototypeBase::custom_tooltip_fields::name` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPrototypeBase::custom_tooltip_fields::name` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPrototypeBase::custom_tooltip_fields::value` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPrototypeBase::custom_tooltip_fields::value` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPrototypeBase::custom_tooltip_fields::quality_values` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPrototypeBase::custom_tooltip_fields::quality_values` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPrototypes::utility_constants::factoriopedia_recycling_recipe_categories` - concept 'RecipeCategoryID' accepts several incompatible value kinds; mapped to object  
  _(LuaRecipeCategoryPrototype|string)_
* [runtime] `LuaRecipe::ingredients::quality_min` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaRecipe::ingredients::quality_max` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaRecord::build_blueprint::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRecord::build_blueprint::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRecord::build_blueprint::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRecord::cancel_deconstruct_area::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRecord::cancel_deconstruct_area::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRecord::cancel_deconstruct_area::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRecord::create_blueprint::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRecord::create_blueprint::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRecord::deconstruct_area::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRecord::deconstruct_area::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRecord::deconstruct_area::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRendering::draw_animation::target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_animation::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRendering::draw_animation::orientation_target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_animation::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_animation::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_animation::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRendering::draw_animation::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRendering::draw_arc::target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_arc::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRendering::draw_arc::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_arc::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_arc::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRendering::draw_arc::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRendering::draw_circle::target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_circle::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRendering::draw_circle::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_circle::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_circle::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRendering::draw_circle::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRendering::draw_light::target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_light::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRendering::draw_light::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_light::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_light::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRendering::draw_light::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRendering::draw_line::from` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_line::to` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_line::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRendering::draw_line::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_line::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_line::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRendering::draw_line::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRendering::draw_polygon::vertices` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_polygon::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRendering::draw_polygon::target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_polygon::orientation_target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_polygon::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_polygon::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_polygon::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRendering::draw_polygon::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRendering::draw_rectangle::left_top` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_rectangle::right_bottom` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_rectangle::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRendering::draw_rectangle::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_rectangle::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_rectangle::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRendering::draw_rectangle::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRendering::draw_sprite::target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_sprite::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRendering::draw_sprite::orientation_target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_sprite::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_sprite::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_sprite::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRendering::draw_sprite::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRendering::draw_text::text` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaRendering::draw_text::text` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaRendering::draw_text::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRendering::draw_text::target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_text::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_text::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_text::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRendering::draw_text::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaSegmentedUnit::clone::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSegmentedUnit::get_ai_state::return::last_damage_time` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSegmentedUnit::get_ai_state::return::destination` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSegmentedUnit::get_ai_state::return::destination` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSegmentedUnit::get_ai_state::return::remaining_time` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSegmentedUnit::get_ai_state::return::target` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSegmentedUnit::get_ai_state::return::destination` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSegmentedUnit::get_ai_state::return::destination` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSimulation::get_widget_position::item` - concept 'ItemID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemPrototype|LuaItemStack|LuaItem|string)_
* [runtime] `LuaSimulation::get_widget_position::item` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSimulation::get_widget_position::recipe` - concept 'RecipeID' accepts several incompatible value kinds; mapped to object  
  _(LuaRecipePrototype|LuaRecipe|string)_
* [runtime] `LuaSimulation::get_widget_position::filter` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSimulation::get_widget_position::request_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSimulation::get_widget_position::text` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaSimulation::get_widget_position::text` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaSimulation::get_widget_position::text` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSimulation::get_widget_position::text` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSimulation::get_widget_position::text` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSimulation::get_widget_position::group` - concept 'ItemGroupID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemGroup|string)_
* [runtime] `LuaSimulation::get_widget_position::text` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSimulation::get_widget_position::text` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSimulation::get_widget_position::text` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSpacePlatform::destroy_asteroid_chunks::name` - concept 'AsteroidChunkID' accepts several incompatible value kinds; mapped to object  
  _(LuaAsteroidChunkPrototype|string)_
* [runtime] `LuaSpacePlatform::destroy_asteroid_chunks::name` - concept 'AsteroidChunkID' accepts several incompatible value kinds; mapped to object  
  _(LuaAsteroidChunkPrototype|string)_
* [runtime] `LuaSpacePlatform::find_asteroid_chunks_filtered::name` - concept 'AsteroidChunkID' accepts several incompatible value kinds; mapped to object  
  _(LuaAsteroidChunkPrototype|string)_
* [runtime] `LuaSpacePlatform::find_asteroid_chunks_filtered::name` - concept 'AsteroidChunkID' accepts several incompatible value kinds; mapped to object  
  _(LuaAsteroidChunkPrototype|string)_
* [runtime] `LuaSurface::can_fast_replace::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::can_fast_replace::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::can_place_entity::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::can_place_entity::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::cancel_deconstruct_area::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::cancel_deconstruct_area::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaSurface::cancel_upgrade_area::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::cancel_upgrade_area::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaSurface::clone_area::destination_surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaSurface::clone_area::destination_force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::clone_brush::destination_surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaSurface::clone_entities::destination_surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaSurface::clone_entities::destination_force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::count_entities_filtered::filter::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::count_entities_filtered::filter::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::count_entities_filtered::filter::ghost_name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::count_entities_filtered::filter::ghost_name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::count_entities_filtered::filter::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::count_entities_filtered::filter::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::count_entities_filtered::filter::force` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaSurface::count_entities_filtered::filter::has_item_inside` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaSurface::count_tiles_filtered::filter::name` - concept 'TileID' accepts several incompatible value kinds; mapped to object  
  _(LuaTilePrototype|LuaTile|string)_
* [runtime] `LuaSurface::count_tiles_filtered::filter::name` - concept 'TileID' accepts several incompatible value kinds; mapped to object  
  _(LuaTilePrototype|LuaTile|string)_
* [runtime] `LuaSurface::count_tiles_filtered::filter::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::count_tiles_filtered::filter::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::count_tiles_filtered::filter::force` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaSurface::create_entities_from_blueprint_string::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::create_entities_from_blueprint_string::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaSurface::create_entity::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::result-inventory` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::player_description` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::player_description` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::text` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaSurface::create_entity::text` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaSurface::create_entity::priority-list` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::inner_name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::priority-list` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::filter` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::movement` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::speed` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::rail_layer` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::recipe` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::player_description` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::render_player_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::bar` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::text` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::filter` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::target_position` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::priority-list` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::bar` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::ignore-unprioritised` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaSurface::create_entity::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaSurface::create_entity::ignore-unprioritised` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::bar` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::bar` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::height` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::recipe_quality` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::input_priority` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::source_position` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::ignore-unprioritised` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::render_player_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::filter_mode` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::filter_mode` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::vertical_speed` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::variation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::output_priority` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::trunk_inventory` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::infinity_settings` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::belt_stack_size_override` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::frame_speed` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::ammo_inventory` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaSurface::create_entity::copy_color_from_train_stop` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::copy_color_from_train_stop` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::copy_color_from_train_stop` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::copy_color_from_train_stop` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::driver_is_main_gunner` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::create_entity::auto_connect` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::auto_connect` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::auto_connect` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::auto_connect` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::auto_connect` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::target` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::artillery_auto_targeting` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::inventory` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::selected_gun_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::infinity_settings` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::cause` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaSurface::create_particle::name` - concept 'ParticleID' accepts several incompatible value kinds; mapped to object  
  _(LuaParticlePrototype|string)_
* [runtime] `LuaSurface::create_segmented_unit::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::create_segmented_unit::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaSurface::create_segmented_unit::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::create_trivial_smoke::name` - concept 'TrivialSmokeID' accepts several incompatible value kinds; mapped to object  
  _(LuaTrivialSmokePrototype|string)_
* [runtime] `LuaSurface::create_unit_group::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::deconstruct_area::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::deconstruct_area::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaSurface::destroy_decoratives::name` - concept 'DecorativeID' accepts several incompatible value kinds; mapped to object  
  _(LuaDecorativePrototype|string)_
* [runtime] `LuaSurface::destroy_decoratives::name` - concept 'DecorativeID' accepts several incompatible value kinds; mapped to object  
  _(LuaDecorativePrototype|string)_
* [runtime] `LuaSurface::execute_lightning::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::find_decoratives_filtered::name` - concept 'DecorativeID' accepts several incompatible value kinds; mapped to object  
  _(LuaDecorativePrototype|string)_
* [runtime] `LuaSurface::find_decoratives_filtered::name` - concept 'DecorativeID' accepts several incompatible value kinds; mapped to object  
  _(LuaDecorativePrototype|string)_
* [runtime] `LuaSurface::find_nearest_enemy::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::find_nearest_enemy_entity_with_owner::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::find_units::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::request_path::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::set_multi_command::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::spill_inventory::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::spill_item_stack::stack` - concept 'ItemStackIdentification' accepts several incompatible value kinds; mapped to object  
  _(string|ItemStackDefinition|LuaItemStack)_
* [runtime] `LuaSurface::spill_item_stack::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::upgrade_area::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::upgrade_area::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::ammo_category` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::effect_description` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaTechnologyPrototype::effects::effect_description` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::recipe` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::quality` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::research_trigger::entity` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::research_trigger::item` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::research_trigger::trigger_description` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaTechnologyPrototype::research_trigger::trigger_description` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaTrainManager::get_train_stops::filter::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaTrainManager::get_train_stops::filter::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaTrainManager::get_train_stops::filter::type` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaTrainManager::get_train_stops::filter::type` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaTrainManager::get_trains::filter::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaTrainManager::get_trains::filter::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaTrainManager::get_trains::filter::stock` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaTrainManager::get_trains::filter::stock` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaTrainManager::request_train_path::goals` - concept 'TrainPathFinderGoal' accepts several incompatible value kinds; mapped to object  
  _(TrainStopGoal|RailEndGoal|LuaRailEnd|LuaEntity)_
* [runtime] `LuaUndoRedoStack::get_redo_item::return::target` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::position` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::target` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::target` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::a` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::target` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::target` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaUndoRedoStack::get_redo_item::return::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaUndoRedoStack::get_redo_item::return::surface_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::surface_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::surface_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::surface_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::surface_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::b` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::surface_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::surface_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::previous_tile` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `event on_blueprint_settings_pasted::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `event on_blueprint_settings_pasted::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `event on_built_entity::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `event on_built_entity::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `event on_robot_built_entity::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `event on_robot_built_entity::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `event on_space_platform_built_entity::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `event on_space_platform_built_entity::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `event on_string_translated::localised_string` - recursive concept 'LocalisedString' mapped to object
* [runtime] `event on_string_translated::localised_string` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `event script_raised_revive::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `event script_raised_revive::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaEntityPrototype::action::action_delivery::projectile` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaEntityPrototype::action::action_delivery::starting_speed` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaEntityPrototype::action::action_delivery::starting_speed_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaEntityPrototype::action::action_delivery::direction_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaEntityPrototype::action::action_delivery::range_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaEntityPrototype::action::action_delivery::source_offset` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGameScript::default_map_gen_settings::autoplace_controls::frequency` - concept 'MapGenSize' accepts several incompatible value kinds; mapped to object  
  _(float|literal(string:none)|literal(string:very-low)|literal(string:very-small)|literal(string:very-poor)|literal(string:low)|literal(string:small)|literal(string:poor)|literal(string:normal)|literal(string:medium)|literal(string:regular)|literal(string:high)|literal(string:big)|literal(string:good)|literal(string:very-high)|literal(string:very-big)|literal(string:very-good))_
* [runtime] `LuaGameScript::default_map_gen_settings::autoplace_controls::size` - concept 'MapGenSize' accepts several incompatible value kinds; mapped to object  
  _(float|literal(string:none)|literal(string:very-low)|literal(string:very-small)|literal(string:very-poor)|literal(string:low)|literal(string:small)|literal(string:poor)|literal(string:normal)|literal(string:medium)|literal(string:regular)|literal(string:high)|literal(string:big)|literal(string:good)|literal(string:very-high)|literal(string:very-big)|literal(string:very-good))_
* [runtime] `LuaGameScript::default_map_gen_settings::autoplace_controls::richness` - concept 'MapGenSize' accepts several incompatible value kinds; mapped to object  
  _(float|literal(string:none)|literal(string:very-low)|literal(string:very-small)|literal(string:very-poor)|literal(string:low)|literal(string:small)|literal(string:poor)|literal(string:normal)|literal(string:medium)|literal(string:regular)|literal(string:high)|literal(string:big)|literal(string:good)|literal(string:very-high)|literal(string:very-big)|literal(string:very-good))_
* [runtime] `LuaGameScript::default_map_gen_settings::cliff_settings::richness` - concept 'MapGenSize' accepts several incompatible value kinds; mapped to object  
  _(float|literal(string:none)|literal(string:very-low)|literal(string:very-small)|literal(string:very-poor)|literal(string:low)|literal(string:small)|literal(string:poor)|literal(string:normal)|literal(string:medium)|literal(string:regular)|literal(string:high)|literal(string:big)|literal(string:good)|literal(string:very-high)|literal(string:very-big)|literal(string:very-good))_
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filters::name` - concept 'ItemID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemPrototype|LuaItemStack|LuaItem|string)_
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filters::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaItemCommon::get_blueprint_entities::return::grid::equipment` - concept 'EquipmentWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEquipmentPrototype|LuaEquipment|string|EquipmentIDAndQualityIDPair)_
* [runtime] `LuaLogisticSection::filters::value::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaPrototypes::utility_constants::main_menu_simulations::planet` - concept 'SpaceLocationID' accepts several incompatible value kinds; mapped to object  
  _(LuaSpaceLocationPrototype|string)_
* [runtime] `LuaPrototypes::utility_constants::color_filters::localised_name` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPrototypes::utility_constants::color_filters::localised_name` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaUndoRedoStack::get_redo_item::return::grid::id` - concept 'EquipmentWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEquipmentPrototype|LuaEquipment|string|EquipmentIDAndQualityIDPair)_
* [runtime] `LuaAccumulatorControlBehavior::output_signal::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::entity_name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::entity_name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::entity_name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::damage` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::radius` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offsets` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offsets` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offsets` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offsets` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offsets` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offsets` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::radius` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::radius` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offset_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offset_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offset_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offset_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offset_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::offset_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::trigger_created_entity` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::trigger_created_entity` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::trigger_created_entity` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::tile_collision_mask` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::trigger_created_entity` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::check_buildability` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::check_buildability` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::initial_height` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::check_buildability` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::tile_collision_mask` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::max_distance` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::only_when_visible` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::only_when_visible` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::only_when_visible` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::apply_projection` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::tile_collision_mask` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::tile_collision_mask` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::tile_collision_mask` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::tile_collision_mask` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::as_enemy` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::as_enemy` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::as_enemy` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::ignore_no_enemies_mode` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::ignore_no_enemies_mode` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::ignore_no_enemies_mode` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::protected` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::protected` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::protected` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::find_non_colliding_position` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::find_non_colliding_position` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::find_non_colliding_position` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::speed_from_center` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::abort_if_over_space` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::abort_if_over_space` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::abort_if_over_space` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::speed_from_center_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::non_colliding_search_radius` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::non_colliding_search_radius` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::non_colliding_search_radius` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::non_colliding_search_precision` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::non_colliding_search_precision` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::non_colliding_search_precision` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::non_colliding_fail_result` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::non_colliding_fail_result` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::non_colliding_fail_result` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::initial_height` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::speed` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::only_when_visible` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::speed_multiplier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::speed_multiplier_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::starting_frame` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::starting_frame_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::speed_from_center` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::dying_trigger_effect::speed_from_center_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaAsteroidChunkPrototype::mineable_properties::products` - concept 'Product' accepts several incompatible value kinds; mapped to object  
  _(ItemProduct|FluidProduct)_
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::get_event_filter::return::name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaBootstrap::raise_player_crafted_item::recipe` - concept 'RecipeID' accepts several incompatible value kinds; mapped to object  
  _(LuaRecipePrototype|LuaRecipe|string)_
* [runtime] `LuaBootstrap::raise_script_revive::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaBootstrap::raise_script_revive::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaBurner::currently_burning::name` - concept 'ItemID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemPrototype|LuaItemStack|LuaItem|string)_
* [runtime] `LuaBurner::currently_burning::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaCommandable::command::destination` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::distraction` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::destination` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::distraction` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::distraction` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::radius` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::distraction` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::distraction` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::distraction` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::distraction` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::ticks_to_wait` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaCommandable::command::radius` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaControl::begin_crafting::recipe` - concept 'RecipeID' accepts several incompatible value kinds; mapped to object  
  _(LuaRecipePrototype|LuaRecipe|string)_
* [runtime] `LuaControl::can_insert::items::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaControl::can_insert::items::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaControl::can_insert::items::custom_description` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaControl::can_insert::items::custom_description` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaControl::can_place_entity::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaControl::get_item_count::item::name` - concept 'ItemID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemPrototype|LuaItemStack|LuaItem|string)_
* [runtime] `LuaControl::get_item_count::item::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaElectricNetwork::get_accumulators_energy::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaElectricNetwork::get_accumulators_energy::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaElectricNetwork::set_accumulators_energy::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaElectricNetwork::set_accumulators_energy::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaElectricSubNetwork::get_accumulators_energy::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaElectricSubNetwork::get_accumulators_energy::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaElectricSubNetwork::set_accumulators_energy::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaElectricSubNetwork::set_accumulators_energy::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntity::cargo_pod_destination::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaEntity::custom_status::label` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaEntity::custom_status::label` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaEntity::infinity_container_filters::name` - concept 'ItemID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemPrototype|LuaItemStack|LuaItem|string)_
* [runtime] `LuaEntity::infinity_container_filters::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntity::previous_recipe::name` - concept 'RecipeID' accepts several incompatible value kinds; mapped to object  
  _(LuaRecipePrototype|LuaRecipe|string)_
* [runtime] `LuaEntity::previous_recipe::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntity::apply_upgrade::override_target::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaEntity::apply_upgrade::override_target::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntity::clone::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaEntity::create_cargo_pod::cargo_pod_prototype` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaEntity::destroy::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaEntity::flip::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaEntity::get_stored_durability::return::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntity::get_tooltip_field::return::name` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaEntity::get_tooltip_field::return::name` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaEntity::get_tooltip_field::return::value` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaEntity::get_tooltip_field::return::value` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaEntity::order_upgrade::target` - concept 'EntityWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|LuaEntityPrototype|string|EntityIDAndQualityIDPair)_
* [runtime] `LuaEntity::order_upgrade::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaEntity::order_upgrade::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaEntity::rotate::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaEntityPrototype::attack_parameters::projectile_creation_parameters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaEntityPrototype::hatch_definitions::cargo_unit_entity_to_spawn` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaEntityPrototype::hatch_definitions::receiving_cargo_units` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaEntityPrototype::loot::quality_min` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEntityPrototype::loot::quality_max` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEquipmentGrid::count::equipment::name` - concept 'EquipmentID' accepts several incompatible value kinds; mapped to object  
  _(LuaEquipmentPrototype|LuaEquipment|string)_
* [runtime] `LuaEquipmentGrid::count::equipment::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEquipmentGrid::put::name` - concept 'EquipmentID' accepts several incompatible value kinds; mapped to object  
  _(LuaEquipmentPrototype|LuaEquipment|string)_
* [runtime] `LuaEquipmentGrid::put::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaEquipmentGrid::put::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaEquipmentGrid::take::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaFlowStatistics::get_flow_count::name` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaFlowStatistics::get_flow_count::name` - concept 'FluidID' accepts several incompatible value kinds; mapped to object  
  _(string|LuaFluidPrototype|Fluid)_
* [runtime] `LuaFlowStatistics::get_flow_count::name` - concept 'EntityWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|LuaEntityPrototype|string|EntityIDAndQualityIDPair)_
* [runtime] `LuaFlowStatistics::get_flow_count::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaForce::add_chart_tag::tag::last_user` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaForce::create_space_platform::planet` - concept 'SpaceLocationID' accepts several incompatible value kinds; mapped to object  
  _(LuaSpaceLocationPrototype|string)_
* [runtime] `LuaForce::create_space_platform::starter_pack` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaForce::remove_alert::filter::prototype` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaForce::remove_alert::filter::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaForce::remove_alert::filter::message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaForce::remove_alert::filter::message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::default_map_gen_settings::starting_area` - concept 'MapGenSize' accepts several incompatible value kinds; mapped to object  
  _(float|literal(string:none)|literal(string:very-low)|literal(string:very-small)|literal(string:very-poor)|literal(string:low)|literal(string:small)|literal(string:poor)|literal(string:normal)|literal(string:medium)|literal(string:regular)|literal(string:high)|literal(string:big)|literal(string:good)|literal(string:very-high)|literal(string:very-big)|literal(string:very-good))_
* [runtime] `LuaGameScript::get_vehicles::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaGameScript::get_vehicles::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaGameScript::get_vehicles::type` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaGameScript::get_vehicles::type` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaGameScript::set_lose_ending_info::title` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::set_lose_ending_info::title` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::set_lose_ending_info::message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::set_lose_ending_info::message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::set_lose_ending_info::bullet_points` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::set_lose_ending_info::bullet_points` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::set_lose_ending_info::final_message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::set_lose_ending_info::final_message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::set_win_ending_info::title` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::set_win_ending_info::title` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::set_win_ending_info::message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::set_win_ending_info::message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::set_win_ending_info::bullet_points` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::set_win_ending_info::bullet_points` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::set_win_ending_info::final_message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::set_win_ending_info::final_message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::show_message_dialog::text` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGameScript::show_message_dialog::text` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGameScript::take_screenshot::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaGameScript::take_screenshot::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaGameScript::take_screenshot::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaGameScript::take_technology_screenshot::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaGameScript::take_technology_screenshot::selected_technology` - concept 'TechnologyID' accepts several incompatible value kinds; mapped to object  
  _(LuaTechnologyPrototype|LuaTechnology|string)_
* [runtime] `LuaGuiElement::elem_filters::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::elem_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::elem_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::elem_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::elem_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::elem_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::elem_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::elem_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::elem_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::comparison` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::elem_filters::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::empty_slot_info::caption` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::empty_slot_info::caption` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::empty_slot_info::text` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::empty_slot_info::text` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add::direction` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::state` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::sprite` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::items` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::add::items` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add::direction` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::items` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::text` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::position` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::badge_text` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::add::badge_text` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add::selected_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::surface_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::caption` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::add::caption` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add::value` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::left_label_caption` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::add::left_label_caption` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add::tooltip` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::add::tooltip` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add::left_label_tooltip` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::add::left_label_tooltip` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add::zoom` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::right_label_caption` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::add::right_label_caption` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add::right_label_tooltip` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaGuiElement::add::right_label_tooltip` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaGuiElement::add::icon_selector` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::mouse_button_filter` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::auto_toggle` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::toggled` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGuiElement::add::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaGuiElement::add::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaItemCommon::build_blueprint::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaItemCommon::build_blueprint::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaItemCommon::build_blueprint::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaItemCommon::cancel_deconstruct_area::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaItemCommon::cancel_deconstruct_area::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaItemCommon::cancel_deconstruct_area::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaItemCommon::create_blueprint::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaItemCommon::create_blueprint::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaItemCommon::deconstruct_area::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaItemCommon::deconstruct_area::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaItemCommon::deconstruct_area::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::result-inventory` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::stock_connections` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::player_description` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::player_description` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::priority-list` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::priority-list` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::stock_connections` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::stock_connections` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::stock_connections` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filter` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::rail_layer` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::recipe` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::player_description` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::bar` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filter` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::priority-list` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::artillery_auto_targeting` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::bar` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::ignore-unprioritised` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::ignore-unprioritised` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::bar` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filter_mode` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filter_mode` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::bar` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::recipe_quality` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::input_priority` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::ignore-unprioritised` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::output_priority` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::infinity_settings` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::belt_stack_size_override` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::trunk_inventory` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::ammo_inventory` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::copy_color_from_train_stop` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::copy_color_from_train_stop` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::driver_is_main_gunner` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::copy_color_from_train_stop` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::inventory` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::copy_color_from_train_stop` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaItemCommon::get_blueprint_entities::return::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaItemCommon::get_blueprint_entities::return::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::infinity_settings` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::selected_gun_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemCommon::get_blueprint_entities::return::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemPrototype::capsule_action::attack_parameters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaItemPrototype::capsule_action::attack_parameters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaLogisticNetwork::select_drop_point::stack` - concept 'ItemStackIdentification' accepts several incompatible value kinds; mapped to object  
  _(string|ItemStackDefinition|LuaItemStack)_
* [runtime] `LuaLogisticNetwork::select_pickup_point::name` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaLogisticSection::filters::import_from` - concept 'SpaceLocationID' accepts several incompatible value kinds; mapped to object  
  _(LuaSpaceLocationPrototype|string)_
* [runtime] `LuaPlayer::add_pin::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaPlayer::add_pin::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaPlayer::add_pin::resource` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaPlayer::connect_to_server::name` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPlayer::connect_to_server::name` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPlayer::connect_to_server::description` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPlayer::connect_to_server::description` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPlayer::create_local_flying_text::text` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPlayer::create_local_flying_text::text` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPlayer::create_local_flying_text::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaPlayer::get_alerts::return::message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPlayer::get_alerts::return::message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPlayer::set_controller::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaPrototypeBase::custom_tooltip_fields::name` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPrototypeBase::custom_tooltip_fields::name` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPrototypeBase::custom_tooltip_fields::value` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPrototypeBase::custom_tooltip_fields::value` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPrototypeBase::custom_tooltip_fields::quality_values` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPrototypeBase::custom_tooltip_fields::quality_values` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaPrototypes::utility_constants::factoriopedia_recycling_recipe_categories` - concept 'RecipeCategoryID' accepts several incompatible value kinds; mapped to object  
  _(LuaRecipeCategoryPrototype|string)_
* [runtime] `LuaRecipe::ingredients::quality_min` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaRecipe::ingredients::quality_max` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaRecord::build_blueprint::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRecord::build_blueprint::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRecord::build_blueprint::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRecord::cancel_deconstruct_area::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRecord::cancel_deconstruct_area::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRecord::cancel_deconstruct_area::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRecord::create_blueprint::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRecord::create_blueprint::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRecord::deconstruct_area::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRecord::deconstruct_area::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRecord::deconstruct_area::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRendering::draw_animation::target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_animation::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRendering::draw_animation::orientation_target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_animation::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_animation::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_animation::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRendering::draw_animation::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRendering::draw_arc::target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_arc::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRendering::draw_arc::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_arc::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_arc::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRendering::draw_arc::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRendering::draw_circle::target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_circle::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRendering::draw_circle::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_circle::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_circle::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRendering::draw_circle::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRendering::draw_light::target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_light::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRendering::draw_light::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_light::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_light::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRendering::draw_light::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRendering::draw_line::from` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_line::to` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_line::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRendering::draw_line::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_line::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_line::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRendering::draw_line::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRendering::draw_polygon::vertices` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_polygon::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRendering::draw_polygon::target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_polygon::orientation_target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_polygon::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_polygon::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_polygon::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRendering::draw_polygon::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRendering::draw_rectangle::left_top` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_rectangle::right_bottom` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_rectangle::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRendering::draw_rectangle::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_rectangle::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_rectangle::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRendering::draw_rectangle::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRendering::draw_sprite::target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_sprite::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRendering::draw_sprite::orientation_target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_sprite::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_sprite::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_sprite::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRendering::draw_sprite::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaRendering::draw_text::text` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaRendering::draw_text::text` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaRendering::draw_text::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaRendering::draw_text::target` - concept 'ScriptRenderTarget' accepts several incompatible value kinds; mapped to object  
  _(LuaEntity|MapPosition|ScriptRenderTargetTable)_
* [runtime] `LuaRendering::draw_text::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_text::forces` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaRendering::draw_text::forces` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaRendering::draw_text::players` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaSegmentedUnit::clone::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSegmentedUnit::get_ai_state::return::last_damage_time` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSegmentedUnit::get_ai_state::return::destination` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSegmentedUnit::get_ai_state::return::destination` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSegmentedUnit::get_ai_state::return::remaining_time` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSegmentedUnit::get_ai_state::return::target` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSegmentedUnit::get_ai_state::return::destination` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSegmentedUnit::get_ai_state::return::destination` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSimulation::get_widget_position::item` - concept 'ItemID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemPrototype|LuaItemStack|LuaItem|string)_
* [runtime] `LuaSimulation::get_widget_position::item` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSimulation::get_widget_position::recipe` - concept 'RecipeID' accepts several incompatible value kinds; mapped to object  
  _(LuaRecipePrototype|LuaRecipe|string)_
* [runtime] `LuaSimulation::get_widget_position::filter` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSimulation::get_widget_position::request_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSimulation::get_widget_position::text` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaSimulation::get_widget_position::text` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaSimulation::get_widget_position::text` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSimulation::get_widget_position::text` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSimulation::get_widget_position::text` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSimulation::get_widget_position::group` - concept 'ItemGroupID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemGroup|string)_
* [runtime] `LuaSimulation::get_widget_position::text` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSimulation::get_widget_position::text` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSimulation::get_widget_position::text` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSpacePlatform::destroy_asteroid_chunks::name` - concept 'AsteroidChunkID' accepts several incompatible value kinds; mapped to object  
  _(LuaAsteroidChunkPrototype|string)_
* [runtime] `LuaSpacePlatform::destroy_asteroid_chunks::name` - concept 'AsteroidChunkID' accepts several incompatible value kinds; mapped to object  
  _(LuaAsteroidChunkPrototype|string)_
* [runtime] `LuaSpacePlatform::find_asteroid_chunks_filtered::name` - concept 'AsteroidChunkID' accepts several incompatible value kinds; mapped to object  
  _(LuaAsteroidChunkPrototype|string)_
* [runtime] `LuaSpacePlatform::find_asteroid_chunks_filtered::name` - concept 'AsteroidChunkID' accepts several incompatible value kinds; mapped to object  
  _(LuaAsteroidChunkPrototype|string)_
* [runtime] `LuaSurface::can_fast_replace::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::can_fast_replace::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::can_place_entity::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::can_place_entity::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::cancel_deconstruct_area::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::cancel_deconstruct_area::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaSurface::cancel_upgrade_area::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::cancel_upgrade_area::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaSurface::clone_area::destination_surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaSurface::clone_area::destination_force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::clone_brush::destination_surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaSurface::clone_entities::destination_surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaSurface::clone_entities::destination_force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::count_entities_filtered::filter::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::count_entities_filtered::filter::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::count_entities_filtered::filter::ghost_name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::count_entities_filtered::filter::ghost_name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::count_entities_filtered::filter::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::count_entities_filtered::filter::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::count_entities_filtered::filter::force` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaSurface::count_entities_filtered::filter::has_item_inside` - concept 'ItemWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemStack|LuaItemPrototype|string|ItemIDAndQualityIDPair)_
* [runtime] `LuaSurface::count_tiles_filtered::filter::name` - concept 'TileID' accepts several incompatible value kinds; mapped to object  
  _(LuaTilePrototype|LuaTile|string)_
* [runtime] `LuaSurface::count_tiles_filtered::filter::name` - concept 'TileID' accepts several incompatible value kinds; mapped to object  
  _(LuaTilePrototype|LuaTile|string)_
* [runtime] `LuaSurface::count_tiles_filtered::filter::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::count_tiles_filtered::filter::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::count_tiles_filtered::filter::force` - concept 'ForceSet' accepts several incompatible value kinds; mapped to object  
  _(array[ForceID]|ForceID)_
* [runtime] `LuaSurface::create_entities_from_blueprint_string::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::create_entities_from_blueprint_string::by_player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaSurface::create_entity::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::result-inventory` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::player_description` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::player_description` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::text` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaSurface::create_entity::text` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaSurface::create_entity::priority-list` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::inner_name` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::priority-list` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::filter` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::movement` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::speed` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::rail_layer` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::recipe` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::player_description` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::render_player_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::bar` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::text` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::filter` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::enable_logistics_while_moving` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::target_position` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::priority-list` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::type` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::bar` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::ignore-unprioritised` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaSurface::create_entity::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaSurface::create_entity::ignore-unprioritised` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::bar` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::bar` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::height` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::recipe_quality` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::input_priority` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::grid` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::source_position` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::ignore-unprioritised` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::render_player_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::filter_mode` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::filter_mode` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::vertical_speed` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::variation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::output_priority` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::trunk_inventory` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::infinity_settings` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::belt_stack_size_override` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::frame_speed` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::ammo_inventory` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaSurface::create_entity::copy_color_from_train_stop` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::copy_color_from_train_stop` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::copy_color_from_train_stop` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::copy_color_from_train_stop` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::driver_is_main_gunner` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::create_entity::auto_connect` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::auto_connect` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::auto_connect` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::auto_connect` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::auto_connect` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::target` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::artillery_auto_targeting` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::inventory` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::selected_gun_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::orientation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::infinity_settings` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::request_filters` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::cause` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::create_entity::color` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::control_behavior` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaSurface::create_entity::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaSurface::create_particle::name` - concept 'ParticleID' accepts several incompatible value kinds; mapped to object  
  _(LuaParticlePrototype|string)_
* [runtime] `LuaSurface::create_segmented_unit::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::create_segmented_unit::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaSurface::create_segmented_unit::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::create_trivial_smoke::name` - concept 'TrivialSmokeID' accepts several incompatible value kinds; mapped to object  
  _(LuaTrivialSmokePrototype|string)_
* [runtime] `LuaSurface::create_unit_group::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::deconstruct_area::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::deconstruct_area::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaSurface::destroy_decoratives::name` - concept 'DecorativeID' accepts several incompatible value kinds; mapped to object  
  _(LuaDecorativePrototype|string)_
* [runtime] `LuaSurface::destroy_decoratives::name` - concept 'DecorativeID' accepts several incompatible value kinds; mapped to object  
  _(LuaDecorativePrototype|string)_
* [runtime] `LuaSurface::execute_lightning::name` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaSurface::find_decoratives_filtered::name` - concept 'DecorativeID' accepts several incompatible value kinds; mapped to object  
  _(LuaDecorativePrototype|string)_
* [runtime] `LuaSurface::find_decoratives_filtered::name` - concept 'DecorativeID' accepts several incompatible value kinds; mapped to object  
  _(LuaDecorativePrototype|string)_
* [runtime] `LuaSurface::find_nearest_enemy::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::find_nearest_enemy_entity_with_owner::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::find_units::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::request_path::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::set_multi_command::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::spill_inventory::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::spill_item_stack::stack` - concept 'ItemStackIdentification' accepts several incompatible value kinds; mapped to object  
  _(string|ItemStackDefinition|LuaItemStack)_
* [runtime] `LuaSurface::spill_item_stack::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::upgrade_area::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaSurface::upgrade_area::player` - concept 'PlayerIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaPlayer)_
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::ammo_category` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::effect_description` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaTechnologyPrototype::effects::effect_description` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::recipe` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::quality` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::effects::modifier` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::research_trigger::entity` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::research_trigger::item` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaTechnologyPrototype::research_trigger::trigger_description` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaTechnologyPrototype::research_trigger::trigger_description` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaTrainManager::get_train_stops::filter::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaTrainManager::get_train_stops::filter::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaTrainManager::get_train_stops::filter::type` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaTrainManager::get_train_stops::filter::type` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaTrainManager::get_trains::filter::surface` - concept 'SurfaceIdentification' accepts several incompatible value kinds; mapped to object  
  _(uint32|string|LuaSurface)_
* [runtime] `LuaTrainManager::get_trains::filter::force` - concept 'ForceID' accepts several incompatible value kinds; mapped to object  
  _(string|uint8|LuaForce)_
* [runtime] `LuaTrainManager::get_trains::filter::stock` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaTrainManager::get_trains::filter::stock` - concept 'EntityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEntityPrototype|LuaEntity|string)_
* [runtime] `LuaTrainManager::request_train_path::goals` - concept 'TrainPathFinderGoal' accepts several incompatible value kinds; mapped to object  
  _(TrainStopGoal|RailEndGoal|LuaRailEnd|LuaEntity)_
* [runtime] `LuaUndoRedoStack::get_redo_item::return::target` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::position` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::target` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::target` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::a` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::target` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::target` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `LuaUndoRedoStack::get_redo_item::return::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaUndoRedoStack::get_redo_item::return::surface_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::surface_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::surface_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::surface_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::surface_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::b` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::surface_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::surface_index` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaUndoRedoStack::get_redo_item::return::previous_tile` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `event on_blueprint_settings_pasted::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `event on_blueprint_settings_pasted::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `event on_built_entity::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `event on_built_entity::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `event on_robot_built_entity::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `event on_robot_built_entity::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `event on_space_platform_built_entity::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `event on_space_platform_built_entity::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `event on_string_translated::localised_string` - recursive concept 'LocalisedString' mapped to object
* [runtime] `event on_string_translated::localised_string` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `event script_raised_revive::tags` - 'builtin' (unspecified fundamental) type mapped to object
* [runtime] `event script_raised_revive::tags` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `LuaEntityPrototype::action::action_delivery::projectile` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaEntityPrototype::action::action_delivery::starting_speed` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaEntityPrototype::action::action_delivery::starting_speed_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaEntityPrototype::action::action_delivery::direction_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaEntityPrototype::action::action_delivery::range_deviation` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaEntityPrototype::action::action_delivery::source_offset` - duplicate field from a variant parameter group, merged into one table key
* [runtime] `LuaGameScript::default_map_gen_settings::autoplace_controls::frequency` - concept 'MapGenSize' accepts several incompatible value kinds; mapped to object  
  _(float|literal(string:none)|literal(string:very-low)|literal(string:very-small)|literal(string:very-poor)|literal(string:low)|literal(string:small)|literal(string:poor)|literal(string:normal)|literal(string:medium)|literal(string:regular)|literal(string:high)|literal(string:big)|literal(string:good)|literal(string:very-high)|literal(string:very-big)|literal(string:very-good))_
* [runtime] `LuaGameScript::default_map_gen_settings::autoplace_controls::size` - concept 'MapGenSize' accepts several incompatible value kinds; mapped to object  
  _(float|literal(string:none)|literal(string:very-low)|literal(string:very-small)|literal(string:very-poor)|literal(string:low)|literal(string:small)|literal(string:poor)|literal(string:normal)|literal(string:medium)|literal(string:regular)|literal(string:high)|literal(string:big)|literal(string:good)|literal(string:very-high)|literal(string:very-big)|literal(string:very-good))_
* [runtime] `LuaGameScript::default_map_gen_settings::autoplace_controls::richness` - concept 'MapGenSize' accepts several incompatible value kinds; mapped to object  
  _(float|literal(string:none)|literal(string:very-low)|literal(string:very-small)|literal(string:very-poor)|literal(string:low)|literal(string:small)|literal(string:poor)|literal(string:normal)|literal(string:medium)|literal(string:regular)|literal(string:high)|literal(string:big)|literal(string:good)|literal(string:very-high)|literal(string:very-big)|literal(string:very-good))_
* [runtime] `LuaGameScript::default_map_gen_settings::cliff_settings::richness` - concept 'MapGenSize' accepts several incompatible value kinds; mapped to object  
  _(float|literal(string:none)|literal(string:very-low)|literal(string:very-small)|literal(string:very-poor)|literal(string:low)|literal(string:small)|literal(string:poor)|literal(string:normal)|literal(string:medium)|literal(string:regular)|literal(string:high)|literal(string:big)|literal(string:good)|literal(string:very-high)|literal(string:very-big)|literal(string:very-good))_
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filters::name` - concept 'ItemID' accepts several incompatible value kinds; mapped to object  
  _(LuaItemPrototype|LuaItemStack|LuaItem|string)_
* [runtime] `LuaItemCommon::get_blueprint_entities::return::filters::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaItemCommon::get_blueprint_entities::return::grid::equipment` - concept 'EquipmentWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEquipmentPrototype|LuaEquipment|string|EquipmentIDAndQualityIDPair)_
* [runtime] `LuaLogisticSection::filters::value::quality` - concept 'QualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaQualityPrototype|string)_
* [runtime] `LuaPrototypes::utility_constants::main_menu_simulations::planet` - concept 'SpaceLocationID' accepts several incompatible value kinds; mapped to object  
  _(LuaSpaceLocationPrototype|string)_
* [runtime] `LuaPrototypes::utility_constants::color_filters::localised_name` - recursive concept 'LocalisedString' mapped to object
* [runtime] `LuaPrototypes::utility_constants::color_filters::localised_name` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|number|boolean|LuaObject|nil|array[LocalisedString])_
* [runtime] `LuaUndoRedoStack::get_redo_item::return::grid::id` - concept 'EquipmentWithQualityID' accepts several incompatible value kinds; mapped to object  
  _(LuaEquipmentPrototype|LuaEquipment|string|EquipmentIDAndQualityIDPair)_
* [runtime] `ActiveDefenseEquipmentPrototype::attack_parameters` - concept 'AttackParameters' accepts several incompatible value kinds; mapped to object  
  _(ProjectileAttackParameters|BeamAttackParameters|StreamAttackParameters)_
* [runtime] `AgriculturalTowerPrototype::allowed_effects` - concept 'EffectTypeLimitation' accepts several incompatible value kinds; mapped to object  
  _((literal(string:speed)|literal(string:productivity)|literal(string:consumption)|literal(string:pollution)|literal(string:quality))|array[(literal(string:speed)|literal(string:productivity)|literal(string:consumption)|literal(string:pollution)|literal(string:quality))])_
* [runtime] `AgriculturalTowerPrototype::energy_source` - concept 'EnergySource' accepts several incompatible value kinds; mapped to object  
  _(ElectricEnergySource|BurnerEnergySource|HeatEnergySource|FluidEnergySource|VoidEnergySource)_
* [runtime] `ArtilleryFlarePrototype::ended_in_water_trigger_effect` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `ArtilleryFlarePrototype::regular_trigger_effect` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `ArtilleryProjectilePrototype::action` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `ArtilleryProjectilePrototype::final_action` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `AsteroidChunkPrototype::dying_trigger_effect` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `BeaconPrototype::allowed_effects` - concept 'EffectTypeLimitation' accepts several incompatible value kinds; mapped to object  
  _((literal(string:speed)|literal(string:productivity)|literal(string:consumption)|literal(string:pollution)|literal(string:quality))|array[(literal(string:speed)|literal(string:productivity)|literal(string:consumption)|literal(string:pollution)|literal(string:quality))])_
* [runtime] `BeamPrototype::action` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `BlueprintBookPrototype::inventory_size` - scalar union mapped to object  
  _(ItemStackIndex|literal(string:dynamic))_
* [runtime] `BoilerPrototype::energy_source` - concept 'EnergySource' accepts several incompatible value kinds; mapped to object  
  _(ElectricEnergySource|BurnerEnergySource|HeatEnergySource|FluidEnergySource|VoidEnergySource)_
* [runtime] `BurnerUsagePrototype::empty_slot_caption` - recursive concept 'LocalisedString' mapped to object
* [runtime] `BurnerUsagePrototype::empty_slot_caption` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|array[LocalisedString])_
* [runtime] `BurnerUsagePrototype::empty_slot_description` - recursive concept 'LocalisedString' mapped to object
* [runtime] `BurnerUsagePrototype::empty_slot_description` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|array[LocalisedString])_
* [runtime] `BurnerUsagePrototype::no_fuel_status` - recursive concept 'LocalisedString' mapped to object
* [runtime] `BurnerUsagePrototype::no_fuel_status` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|array[LocalisedString])_
* [runtime] `CapsulePrototype::capsule_action` - concept 'CapsuleAction' accepts several incompatible value kinds; mapped to object  
  _(ThrowCapsuleAction|ActivateEquipmentCapsuleAction|UseOnSelfCapsuleAction|DestroyCliffsCapsuleAction|ArtilleryRemoteCapsuleAction)_
* [runtime] `CaptureRobotPrototype::destroy_action` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `CargoPodPrototype::impact_trigger` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `ChainActiveTriggerPrototype::action` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `CharacterPrototype::tool_attack_result` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `CombatRobotPrototype::attack_parameters` - concept 'AttackParameters' accepts several incompatible value kinds; mapped to object  
  _(ProjectileAttackParameters|BeamAttackParameters|StreamAttackParameters)_
* [runtime] `CombatRobotPrototype::destroy_action` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `CorpsePrototype::animation` - concept 'RotatedAnimationVariations' accepts several incompatible value kinds; mapped to object  
  _(RotatedAnimation|array[RotatedAnimation])_
* [runtime] `CorpsePrototype::animation_overlay` - concept 'RotatedAnimationVariations' accepts several incompatible value kinds; mapped to object  
  _(RotatedAnimation|array[RotatedAnimation])_
* [runtime] `CorpsePrototype::decay_animation` - concept 'RotatedAnimationVariations' accepts several incompatible value kinds; mapped to object  
  _(RotatedAnimation|array[RotatedAnimation])_
* [runtime] `CraftingMachinePrototype::allowed_effects` - concept 'EffectTypeLimitation' accepts several incompatible value kinds; mapped to object  
  _((literal(string:speed)|literal(string:productivity)|literal(string:consumption)|literal(string:pollution)|literal(string:quality))|array[(literal(string:speed)|literal(string:productivity)|literal(string:consumption)|literal(string:pollution)|literal(string:quality))])_
* [runtime] `CraftingMachinePrototype::energy_source` - concept 'EnergySource' accepts several incompatible value kinds; mapped to object  
  _(ElectricEnergySource|BurnerEnergySource|HeatEnergySource|FluidEnergySource|VoidEnergySource)_
* [runtime] `DecorativePrototype::placed_effect` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `DecorativePrototype::trigger_effect` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `DelayedActiveTriggerPrototype::action` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `DeliverImpactCombination::trigger_effect_item` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `EnemySpawnerPrototype::spawn_blocked_trigger` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `EntityPrototype::created_effect` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `EntityWithHealthPrototype::damaged_trigger_effect` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `EntityWithHealthPrototype::dying_trigger_effect` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `ExplosionPrototype::explosion_effect` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `FireFlamePrototype::on_damage_tick_effect` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `FireFlamePrototype::on_fuel_added_action` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `FluidStreamPrototype::action` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `FluidStreamPrototype::initial_action` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `FuelCategory::fuel_value_type` - recursive concept 'LocalisedString' mapped to object
* [runtime] `FuelCategory::fuel_value_type` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|array[LocalisedString])_
* [runtime] `GunPrototype::attack_parameters` - concept 'AttackParameters' accepts several incompatible value kinds; mapped to object  
  _(ProjectileAttackParameters|BeamAttackParameters|StreamAttackParameters)_
* [runtime] `InserterPrototype::energy_source` - concept 'EnergySource' accepts several incompatible value kinds; mapped to object  
  _(ElectricEnergySource|BurnerEnergySource|HeatEnergySource|FluidEnergySource|VoidEnergySource)_
* [runtime] `ItemPrototype::destroyed_by_dropping_trigger` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `LabPrototype::allowed_effects` - concept 'EffectTypeLimitation' accepts several incompatible value kinds; mapped to object  
  _((literal(string:speed)|literal(string:productivity)|literal(string:consumption)|literal(string:pollution)|literal(string:quality))|array[(literal(string:speed)|literal(string:productivity)|literal(string:consumption)|literal(string:pollution)|literal(string:quality))])_
* [runtime] `LabPrototype::energy_source` - concept 'EnergySource' accepts several incompatible value kinds; mapped to object  
  _(ElectricEnergySource|BurnerEnergySource|HeatEnergySource|FluidEnergySource|VoidEnergySource)_
* [runtime] `LandMinePrototype::action` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `LightningPrototype::attractor_hit_effect` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `LightningPrototype::strike_effect` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `MiningDrillPrototype::allowed_effects` - concept 'EffectTypeLimitation' accepts several incompatible value kinds; mapped to object  
  _((literal(string:speed)|literal(string:productivity)|literal(string:consumption)|literal(string:pollution)|literal(string:quality))|array[(literal(string:speed)|literal(string:productivity)|literal(string:consumption)|literal(string:pollution)|literal(string:quality))])_
* [runtime] `MiningDrillPrototype::energy_source` - concept 'EnergySource' accepts several incompatible value kinds; mapped to object  
  _(ElectricEnergySource|BurnerEnergySource|HeatEnergySource|FluidEnergySource|VoidEnergySource)_
* [runtime] `ModData::data` - concept 'AnyBasic' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|number|table)_
* [runtime] `NamedNoiseExpression::expression` - concept 'NoiseExpression' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|double)_
* [runtime] `NamedNoiseExpression::local_expressions` - concept 'NoiseExpression' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|double)_
* [runtime] `NamedNoiseFunction::expression` - concept 'NoiseExpression' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|double)_
* [runtime] `NamedNoiseFunction::local_expressions` - concept 'NoiseExpression' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|double)_
* [runtime] `OffshorePumpPrototype::energy_source` - concept 'EnergySource' accepts several incompatible value kinds; mapped to object  
  _(ElectricEnergySource|BurnerEnergySource|HeatEnergySource|FluidEnergySource|VoidEnergySource)_
* [runtime] `ParticlePrototype::ended_in_water_trigger_effect` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `ParticlePrototype::ended_on_ground_trigger_effect` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `ParticlePrototype::regular_trigger_effect` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `PlanetPrototype::player_effects` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `ProjectilePrototype::action` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `ProjectilePrototype::animation` - concept 'RotatedAnimationVariations' accepts several incompatible value kinds; mapped to object  
  _(RotatedAnimation|array[RotatedAnimation])_
* [runtime] `ProjectilePrototype::final_action` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `ProjectilePrototype::shadow` - concept 'RotatedAnimationVariations' accepts several incompatible value kinds; mapped to object  
  _(RotatedAnimation|array[RotatedAnimation])_
* [runtime] `PrototypeBase::factoriopedia_description` - recursive concept 'LocalisedString' mapped to object
* [runtime] `PrototypeBase::factoriopedia_description` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|array[LocalisedString])_
* [runtime] `PrototypeBase::localised_description` - recursive concept 'LocalisedString' mapped to object
* [runtime] `PrototypeBase::localised_description` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|array[LocalisedString])_
* [runtime] `PrototypeBase::localised_name` - recursive concept 'LocalisedString' mapped to object
* [runtime] `PrototypeBase::localised_name` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|array[LocalisedString])_
* [runtime] `PumpPrototype::energy_source` - concept 'EnergySource' accepts several incompatible value kinds; mapped to object  
  _(ElectricEnergySource|BurnerEnergySource|HeatEnergySource|FluidEnergySource|VoidEnergySource)_
* [runtime] `RadarPrototype::energy_source` - concept 'EnergySource' accepts several incompatible value kinds; mapped to object  
  _(ElectricEnergySource|BurnerEnergySource|HeatEnergySource|FluidEnergySource|VoidEnergySource)_
* [runtime] `ReactorPrototype::energy_source` - concept 'EnergySource' accepts several incompatible value kinds; mapped to object  
  _(ElectricEnergySource|BurnerEnergySource|HeatEnergySource|FluidEnergySource|VoidEnergySource)_
* [runtime] `ReactorPrototype::meltdown_action` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `RecipePrototype::allow_consumption_message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `RecipePrototype::allow_consumption_message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|array[LocalisedString])_
* [runtime] `RecipePrototype::allow_pollution_message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `RecipePrototype::allow_pollution_message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|array[LocalisedString])_
* [runtime] `RecipePrototype::allow_productivity_message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `RecipePrototype::allow_productivity_message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|array[LocalisedString])_
* [runtime] `RecipePrototype::allow_quality_message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `RecipePrototype::allow_quality_message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|array[LocalisedString])_
* [runtime] `RecipePrototype::allow_speed_message` - recursive concept 'LocalisedString' mapped to object
* [runtime] `RecipePrototype::allow_speed_message` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|array[LocalisedString])_
* [runtime] `RecipePrototype::ingredients` - concept 'IngredientPrototype' accepts several incompatible value kinds; mapped to object  
  _(ItemIngredientPrototype|FluidIngredientPrototype)_
* [runtime] `RecipePrototype::results` - concept 'ProductPrototype' accepts several incompatible value kinds; mapped to object  
  _(ItemProductPrototype|FluidProductPrototype)_
* [runtime] `RoboportPrototype::close_door_trigger_effect` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `RoboportPrototype::open_door_trigger_effect` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `RobotWithLogisticInterfacePrototype::destroy_action` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `RocketSiloPrototype::alarm_trigger` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `RocketSiloPrototype::clamps_off_trigger` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `RocketSiloPrototype::clamps_on_trigger` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `RocketSiloPrototype::doors_trigger` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `RocketSiloPrototype::raise_rocket_trigger` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `RocketSiloRocketPrototype::flying_trigger` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `RollingStockPrototype::drive_over_elevated_tie_trigger` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `RollingStockPrototype::drive_over_tie_trigger` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `SegmentedUnitPrototype::attack_parameters` - concept 'AttackParameters' accepts several incompatible value kinds; mapped to object  
  _(ProjectileAttackParameters|BeamAttackParameters|StreamAttackParameters)_
* [runtime] `SegmentedUnitPrototype::revenge_attack_parameters` - concept 'AttackParameters' accepts several incompatible value kinds; mapped to object  
  _(ProjectileAttackParameters|BeamAttackParameters|StreamAttackParameters)_
* [runtime] `SmokeWithTriggerPrototype::action` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `SpacePlatformStarterPackPrototype::trigger` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `SpiderUnitPrototype::attack_parameters` - concept 'AttackParameters' accepts several incompatible value kinds; mapped to object  
  _(ProjectileAttackParameters|BeamAttackParameters|StreamAttackParameters)_
* [runtime] `TechnologyPrototype::effects` - concept 'Modifier' accepts several incompatible value kinds; mapped to object  
  _(InserterStackSizeBonusModifier|BulkInserterCapacityBonusModifier|LaboratorySpeedModifier|CharacterLogisticTrashSlotsModifier|MaximumFollowingRobotsCountModifier|WorkerRobotSpeedModifier|WorkerRobotStorageModifier|TurretAttackModifier|AmmoDamageModifier|GiveItemModifier|GunSpeedModifier|UnlockRecipeModifier|CharacterCraftingSpeedModifier|CharacterMiningSpeedModifier|CharacterRunningSpeedModifier|CharacterBuildDistanceModifier|CharacterItemDropDistanceModifier|CharacterReachDistanceModifier|CharacterResourceReachDistanceModifier|CharacterItemPickupDistanceModifier|CharacterLootPickupDistanceModifier|CharacterInventorySlotsBonusModifier|DeconstructionTimeToLiveModifier|MaxFailedAttemptsPerTickPerConstructionQueueModifier|MaxSuccessfulAttemptsPerTickPerConstructionQueueModifier|CharacterHealthBonusModifier|MiningDrillProductivityBonusModifier|TrainBrakingForceBonusModifier|WorkerRobotBatteryModifier|LaboratoryProductivityModifier|FollowerRobotLifetimeModifier|ArtilleryRangeModifier|NothingModifier|CharacterLogisticRequestsModifier|VehicleLogisticsModifier|UnlockSpaceLocationModifier|UnlockQualityModifier|SpacePlatformsModifier|TravelToSpacePlatformsModifier|CircuitNetworkModifier|CargoLandingPadLimitModifier|MaxCargoBayUnloadingDistanceModifier|ChangeRecipeProductivityModifier|CliffDeconstructionEnabledModifier|MiningWithFluidModifier|RailSupportOnDeepOilOceanModifier|RailPlannerAllowElevatedRailsModifier|BeaconDistributionModifier|CreateGhostOnEntityDeathModifier|BeltStackSizeBonusModifier|UnlockLogisticNetworkModifier)_
* [runtime] `TechnologyPrototype::max_level` - scalar union mapped to object  
  _(uint32|literal(string:infinite))_
* [runtime] `TechnologyPrototype::research_trigger` - concept 'TechnologyTrigger' accepts several incompatible value kinds; mapped to object  
  _(MineEntityTechnologyTrigger|CraftItemTechnologyTrigger|CraftFluidTechnologyTrigger|SendItemToOrbitTechnologyTrigger|CaptureSpawnerTechnologyTrigger|BuildEntityTechnologyTrigger|CreateSpacePlatformTechnologyTrigger|ScriptedTechnologyTrigger)_
* [runtime] `TilePrototype::default_destroyed_dropped_item_trigger` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `TilePrototype::trigger_effect` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `TipsAndTricksItem::skip_trigger` - concept 'TipTrigger' accepts several incompatible value kinds; mapped to object  
  _(OrTipTrigger|AndTipTrigger|SequenceTipTrigger|DependenciesMetTipTrigger|TimeElapsedTipTrigger|TimeSinceLastTipActivationTipTrigger|ResearchTechnologyTipTrigger|ResearchWithSciencePackTipTrigger|UnlockRecipeTipTrigger|CraftItemTipTrigger|BuildEntityTipTrigger|ManualTransferTipTrigger|ModuleTransferTipTrigger|StackTransferTipTrigger|EntityTransferTipTrigger|DropItemTipTrigger|SetRecipeTipTrigger|SetFilterTipTrigger|LimitChestTipTrigger|UsePipetteTipTrigger|SetLogisticRequestTipTrigger|UseConfirmTipTrigger|ToggleShowEntityInfoTipTrigger|GeneratingPowerTipTrigger|LowPowerTipTrigger|PasteEntitySettingsTipTrigger|FastReplaceTipTrigger|GroupAttackTipTrigger|FastBeltBendTipTrigger|BeltTraverseTipTrigger|PlaceEquipmentTipTrigger|ClearCursorTipTrigger|RotateEntityTipTrigger|FlipEntityTipTrigger|AlternativeBuildTipTrigger|GateOverRailBuildTipTrigger|ManualWireDragTipTrigger|ShootTipTrigger|ChangeSurfaceTipTrigger|ApplyStarterPackTipTrigger|MineItemByRobotTipTrigger|BuildEntityByRobotTipTrigger|PlanTrainPathTipTrigger|UseRailPlannerTipTrigger|ToggleRailLayerTipTrigger|EnterVehicleTipTrigger|SendSpidertronTipTrigger|ActivatePasteTipTrigger|KillTipTrigger)_
* [runtime] `TipsAndTricksItem::trigger` - concept 'TipTrigger' accepts several incompatible value kinds; mapped to object  
  _(OrTipTrigger|AndTipTrigger|SequenceTipTrigger|DependenciesMetTipTrigger|TimeElapsedTipTrigger|TimeSinceLastTipActivationTipTrigger|ResearchTechnologyTipTrigger|ResearchWithSciencePackTipTrigger|UnlockRecipeTipTrigger|CraftItemTipTrigger|BuildEntityTipTrigger|ManualTransferTipTrigger|ModuleTransferTipTrigger|StackTransferTipTrigger|EntityTransferTipTrigger|DropItemTipTrigger|SetRecipeTipTrigger|SetFilterTipTrigger|LimitChestTipTrigger|UsePipetteTipTrigger|SetLogisticRequestTipTrigger|UseConfirmTipTrigger|ToggleShowEntityInfoTipTrigger|GeneratingPowerTipTrigger|LowPowerTipTrigger|PasteEntitySettingsTipTrigger|FastReplaceTipTrigger|GroupAttackTipTrigger|FastBeltBendTipTrigger|BeltTraverseTipTrigger|PlaceEquipmentTipTrigger|ClearCursorTipTrigger|RotateEntityTipTrigger|FlipEntityTipTrigger|AlternativeBuildTipTrigger|GateOverRailBuildTipTrigger|ManualWireDragTipTrigger|ShootTipTrigger|ChangeSurfaceTipTrigger|ApplyStarterPackTipTrigger|MineItemByRobotTipTrigger|BuildEntityByRobotTipTrigger|PlanTrainPathTipTrigger|UseRailPlannerTipTrigger|ToggleRailLayerTipTrigger|EnterVehicleTipTrigger|SendSpidertronTipTrigger|ActivatePasteTipTrigger|KillTipTrigger)_
* [runtime] `TurretPrototype::attack_parameters` - concept 'AttackParameters' accepts several incompatible value kinds; mapped to object  
  _(ProjectileAttackParameters|BeamAttackParameters|StreamAttackParameters)_
* [runtime] `UnitPrototype::attack_parameters` - concept 'AttackParameters' accepts several incompatible value kinds; mapped to object  
  _(ProjectileAttackParameters|BeamAttackParameters|StreamAttackParameters)_
* [runtime] `VehiclePrototype::crash_trigger` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `VehiclePrototype::stop_trigger` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `AmmoItemPrototype::ammo_type::action` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `ArtilleryFlarePrototype::ended_in_water_trigger_effect::non_colliding_fail_result` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `ArtilleryFlarePrototype::ended_in_water_trigger_effect::action` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `ArtilleryProjectilePrototype::action::range_effects` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `AsteroidChunkPrototype::minable::results` - concept 'ProductPrototype' accepts several incompatible value kinds; mapped to object  
  _(ItemProductPrototype|FluidProductPrototype)_
* [runtime] `AsteroidChunkPrototype::minable::mining_trigger` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `BlueprintItemPrototype::alt_select::mode` - concept 'SelectionModeFlags' accepts several incompatible value kinds; mapped to object  
  _((literal(string:blueprint)|literal(string:deconstruct)|literal(string:cancel-deconstruct)|literal(string:items)|literal(string:trees)|literal(string:buildable-type)|literal(string:nothing)|literal(string:items-to-place)|literal(string:any-entity)|literal(string:any-tile)|literal(string:same-force)|literal(string:not-same-force)|literal(string:friend)|literal(string:enemy)|literal(string:upgrade)|literal(string:cancel-upgrade)|literal(string:downgrade)|literal(string:entity-with-health)|literal(string:is-military-target)|literal(string:entity-with-owner)|literal(string:avoid-rolling-stock)|literal(string:avoid-vehicle)|literal(string:controllable)|literal(string:controllable-add)|literal(string:controllable-remove)|literal(string:entity-ghost)|literal(string:tile-ghost))|array[(literal(string:blueprint)|literal(string:deconstruct)|literal(string:cancel-deconstruct)|literal(string:items)|literal(string:trees)|literal(string:buildable-type)|literal(string:nothing)|literal(string:items-to-place)|literal(string:any-entity)|literal(string:any-tile)|literal(string:same-force)|literal(string:not-same-force)|literal(string:friend)|literal(string:enemy)|literal(string:upgrade)|literal(string:cancel-upgrade)|literal(string:downgrade)|literal(string:entity-with-health)|literal(string:is-military-target)|literal(string:entity-with-owner)|literal(string:avoid-rolling-stock)|literal(string:avoid-vehicle)|literal(string:controllable)|literal(string:controllable-add)|literal(string:controllable-remove)|literal(string:entity-ghost)|literal(string:tile-ghost))])_
* [runtime] `CapsulePrototype::capsule_action::attack_parameters` - concept 'AttackParameters' accepts several incompatible value kinds; mapped to object  
  _(ProjectileAttackParameters|BeamAttackParameters|StreamAttackParameters)_
* [runtime] `CapsulePrototype::capsule_action::attack_parameters` - concept 'AttackParameters' accepts several incompatible value kinds; mapped to object  
  _(ProjectileAttackParameters|BeamAttackParameters|StreamAttackParameters)_
* [runtime] `CapsulePrototype::capsule_action::attack_parameters` - concept 'AttackParameters' accepts several incompatible value kinds; mapped to object  
  _(ProjectileAttackParameters|BeamAttackParameters|StreamAttackParameters)_
* [runtime] `DecorativePrototype::autoplace::tile_restriction` - concept 'TileIDRestriction' accepts several incompatible value kinds; mapped to object  
  _(TileID|[TileID,TileID])_
* [runtime] `DecorativePrototype::autoplace::probability_expression` - concept 'NoiseExpression' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|double)_
* [runtime] `DecorativePrototype::autoplace::richness_expression` - concept 'NoiseExpression' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|double)_
* [runtime] `DecorativePrototype::autoplace::local_expressions` - concept 'NoiseExpression' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|double)_
* [runtime] `DecorativePrototype::stateless_visualisation::offset_x` - concept 'RangedValue' accepts several incompatible value kinds; mapped to object  
  _([float,float]|float)_
* [runtime] `DecorativePrototype::stateless_visualisation::offset_y` - concept 'RangedValue' accepts several incompatible value kinds; mapped to object  
  _([float,float]|float)_
* [runtime] `DecorativePrototype::stateless_visualisation::offset_z` - concept 'RangedValue' accepts several incompatible value kinds; mapped to object  
  _([float,float]|float)_
* [runtime] `DecorativePrototype::stateless_visualisation::speed_x` - concept 'RangedValue' accepts several incompatible value kinds; mapped to object  
  _([float,float]|float)_
* [runtime] `DecorativePrototype::stateless_visualisation::speed_y` - concept 'RangedValue' accepts several incompatible value kinds; mapped to object  
  _([float,float]|float)_
* [runtime] `DecorativePrototype::stateless_visualisation::speed_z` - concept 'RangedValue' accepts several incompatible value kinds; mapped to object  
  _([float,float]|float)_
* [runtime] `DecorativePrototype::stateless_visualisation::scale` - concept 'RangedValue' accepts several incompatible value kinds; mapped to object  
  _([float,float]|float)_
* [runtime] `EntityWithHealthPrototype::attack_reaction::action` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `ItemPrototype::spoil_to_trigger_result::trigger` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `NamedNoiseExpression::local_functions::expression` - concept 'NoiseExpression' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|double)_
* [runtime] `NamedNoiseExpression::local_functions::local_expressions` - concept 'NoiseExpression' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|double)_
* [runtime] `PlanetPrototype::map_gen_settings::property_expression_names` - scalar union mapped to object  
  _(string|boolean|double)_
* [runtime] `ProcessionPrototype::ground_timeline::layers` - concept 'ProcessionLayer' accepts several incompatible value kinds; mapped to object  
  _(PodDistanceTraveledProcessionLayer|PodMovementProcessionLayer|PodOpacityProcessionLayer|SingleGraphicProcessionLayer|CoverGraphicProcessionLayer|TintProcessionLayer|PodAnimationProcessionLayer)_
* [runtime] `Prototype::custom_tooltip_fields::name` - recursive concept 'LocalisedString' mapped to object
* [runtime] `Prototype::custom_tooltip_fields::name` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|array[LocalisedString])_
* [runtime] `Prototype::custom_tooltip_fields::value` - recursive concept 'LocalisedString' mapped to object
* [runtime] `Prototype::custom_tooltip_fields::value` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|array[LocalisedString])_
* [runtime] `Prototype::custom_tooltip_fields::quality_values` - recursive concept 'LocalisedString' mapped to object
* [runtime] `Prototype::custom_tooltip_fields::quality_values` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|array[LocalisedString])_
* [runtime] `SegmentPrototype::update_effects::effect` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `SpiderLegPrototype::lower_leg_dying_trigger_effects::effect` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `TechnologyPrototype::effects::effect_description` - recursive concept 'LocalisedString' mapped to object
* [runtime] `TechnologyPrototype::effects::effect_description` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|array[LocalisedString])_
* [runtime] `TechnologyPrototype::research_trigger::trigger_description` - recursive concept 'LocalisedString' mapped to object
* [runtime] `TechnologyPrototype::research_trigger::trigger_description` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|array[LocalisedString])_
* [runtime] `TipsAndTricksItem::skip_trigger::triggers` - concept 'TipTrigger' accepts several incompatible value kinds; mapped to object  
  _(OrTipTrigger|AndTipTrigger|SequenceTipTrigger|DependenciesMetTipTrigger|TimeElapsedTipTrigger|TimeSinceLastTipActivationTipTrigger|ResearchTechnologyTipTrigger|ResearchWithSciencePackTipTrigger|UnlockRecipeTipTrigger|CraftItemTipTrigger|BuildEntityTipTrigger|ManualTransferTipTrigger|ModuleTransferTipTrigger|StackTransferTipTrigger|EntityTransferTipTrigger|DropItemTipTrigger|SetRecipeTipTrigger|SetFilterTipTrigger|LimitChestTipTrigger|UsePipetteTipTrigger|SetLogisticRequestTipTrigger|UseConfirmTipTrigger|ToggleShowEntityInfoTipTrigger|GeneratingPowerTipTrigger|LowPowerTipTrigger|PasteEntitySettingsTipTrigger|FastReplaceTipTrigger|GroupAttackTipTrigger|FastBeltBendTipTrigger|BeltTraverseTipTrigger|PlaceEquipmentTipTrigger|ClearCursorTipTrigger|RotateEntityTipTrigger|FlipEntityTipTrigger|AlternativeBuildTipTrigger|GateOverRailBuildTipTrigger|ManualWireDragTipTrigger|ShootTipTrigger|ChangeSurfaceTipTrigger|ApplyStarterPackTipTrigger|MineItemByRobotTipTrigger|BuildEntityByRobotTipTrigger|PlanTrainPathTipTrigger|UseRailPlannerTipTrigger|ToggleRailLayerTipTrigger|EnterVehicleTipTrigger|SendSpidertronTipTrigger|ActivatePasteTipTrigger|KillTipTrigger)_
* [runtime] `TipsAndTricksItem::skip_trigger::triggers` - concept 'TipTrigger' accepts several incompatible value kinds; mapped to object  
  _(OrTipTrigger|AndTipTrigger|SequenceTipTrigger|DependenciesMetTipTrigger|TimeElapsedTipTrigger|TimeSinceLastTipActivationTipTrigger|ResearchTechnologyTipTrigger|ResearchWithSciencePackTipTrigger|UnlockRecipeTipTrigger|CraftItemTipTrigger|BuildEntityTipTrigger|ManualTransferTipTrigger|ModuleTransferTipTrigger|StackTransferTipTrigger|EntityTransferTipTrigger|DropItemTipTrigger|SetRecipeTipTrigger|SetFilterTipTrigger|LimitChestTipTrigger|UsePipetteTipTrigger|SetLogisticRequestTipTrigger|UseConfirmTipTrigger|ToggleShowEntityInfoTipTrigger|GeneratingPowerTipTrigger|LowPowerTipTrigger|PasteEntitySettingsTipTrigger|FastReplaceTipTrigger|GroupAttackTipTrigger|FastBeltBendTipTrigger|BeltTraverseTipTrigger|PlaceEquipmentTipTrigger|ClearCursorTipTrigger|RotateEntityTipTrigger|FlipEntityTipTrigger|AlternativeBuildTipTrigger|GateOverRailBuildTipTrigger|ManualWireDragTipTrigger|ShootTipTrigger|ChangeSurfaceTipTrigger|ApplyStarterPackTipTrigger|MineItemByRobotTipTrigger|BuildEntityByRobotTipTrigger|PlanTrainPathTipTrigger|UseRailPlannerTipTrigger|ToggleRailLayerTipTrigger|EnterVehicleTipTrigger|SendSpidertronTipTrigger|ActivatePasteTipTrigger|KillTipTrigger)_
* [runtime] `TipsAndTricksItem::skip_trigger::triggers` - concept 'TipTrigger' accepts several incompatible value kinds; mapped to object  
  _(OrTipTrigger|AndTipTrigger|SequenceTipTrigger|DependenciesMetTipTrigger|TimeElapsedTipTrigger|TimeSinceLastTipActivationTipTrigger|ResearchTechnologyTipTrigger|ResearchWithSciencePackTipTrigger|UnlockRecipeTipTrigger|CraftItemTipTrigger|BuildEntityTipTrigger|ManualTransferTipTrigger|ModuleTransferTipTrigger|StackTransferTipTrigger|EntityTransferTipTrigger|DropItemTipTrigger|SetRecipeTipTrigger|SetFilterTipTrigger|LimitChestTipTrigger|UsePipetteTipTrigger|SetLogisticRequestTipTrigger|UseConfirmTipTrigger|ToggleShowEntityInfoTipTrigger|GeneratingPowerTipTrigger|LowPowerTipTrigger|PasteEntitySettingsTipTrigger|FastReplaceTipTrigger|GroupAttackTipTrigger|FastBeltBendTipTrigger|BeltTraverseTipTrigger|PlaceEquipmentTipTrigger|ClearCursorTipTrigger|RotateEntityTipTrigger|FlipEntityTipTrigger|AlternativeBuildTipTrigger|GateOverRailBuildTipTrigger|ManualWireDragTipTrigger|ShootTipTrigger|ChangeSurfaceTipTrigger|ApplyStarterPackTipTrigger|MineItemByRobotTipTrigger|BuildEntityByRobotTipTrigger|PlanTrainPathTipTrigger|UseRailPlannerTipTrigger|ToggleRailLayerTipTrigger|EnterVehicleTipTrigger|SendSpidertronTipTrigger|ActivatePasteTipTrigger|KillTipTrigger)_
* [runtime] `UtilityConstants::color_filters::localised_name` - recursive concept 'LocalisedString' mapped to object
* [runtime] `UtilityConstants::color_filters::localised_name` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|array[LocalisedString])_
* [runtime] `AmmoItemPrototype::ammo_type::action` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `ArtilleryFlarePrototype::ended_in_water_trigger_effect::non_colliding_fail_result` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `ArtilleryFlarePrototype::ended_in_water_trigger_effect::action` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `ArtilleryProjectilePrototype::action::range_effects` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `AsteroidChunkPrototype::minable::results` - concept 'ProductPrototype' accepts several incompatible value kinds; mapped to object  
  _(ItemProductPrototype|FluidProductPrototype)_
* [runtime] `AsteroidChunkPrototype::minable::mining_trigger` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `BlueprintItemPrototype::alt_select::mode` - concept 'SelectionModeFlags' accepts several incompatible value kinds; mapped to object  
  _((literal(string:blueprint)|literal(string:deconstruct)|literal(string:cancel-deconstruct)|literal(string:items)|literal(string:trees)|literal(string:buildable-type)|literal(string:nothing)|literal(string:items-to-place)|literal(string:any-entity)|literal(string:any-tile)|literal(string:same-force)|literal(string:not-same-force)|literal(string:friend)|literal(string:enemy)|literal(string:upgrade)|literal(string:cancel-upgrade)|literal(string:downgrade)|literal(string:entity-with-health)|literal(string:is-military-target)|literal(string:entity-with-owner)|literal(string:avoid-rolling-stock)|literal(string:avoid-vehicle)|literal(string:controllable)|literal(string:controllable-add)|literal(string:controllable-remove)|literal(string:entity-ghost)|literal(string:tile-ghost))|array[(literal(string:blueprint)|literal(string:deconstruct)|literal(string:cancel-deconstruct)|literal(string:items)|literal(string:trees)|literal(string:buildable-type)|literal(string:nothing)|literal(string:items-to-place)|literal(string:any-entity)|literal(string:any-tile)|literal(string:same-force)|literal(string:not-same-force)|literal(string:friend)|literal(string:enemy)|literal(string:upgrade)|literal(string:cancel-upgrade)|literal(string:downgrade)|literal(string:entity-with-health)|literal(string:is-military-target)|literal(string:entity-with-owner)|literal(string:avoid-rolling-stock)|literal(string:avoid-vehicle)|literal(string:controllable)|literal(string:controllable-add)|literal(string:controllable-remove)|literal(string:entity-ghost)|literal(string:tile-ghost))])_
* [runtime] `CapsulePrototype::capsule_action::attack_parameters` - concept 'AttackParameters' accepts several incompatible value kinds; mapped to object  
  _(ProjectileAttackParameters|BeamAttackParameters|StreamAttackParameters)_
* [runtime] `CapsulePrototype::capsule_action::attack_parameters` - concept 'AttackParameters' accepts several incompatible value kinds; mapped to object  
  _(ProjectileAttackParameters|BeamAttackParameters|StreamAttackParameters)_
* [runtime] `CapsulePrototype::capsule_action::attack_parameters` - concept 'AttackParameters' accepts several incompatible value kinds; mapped to object  
  _(ProjectileAttackParameters|BeamAttackParameters|StreamAttackParameters)_
* [runtime] `DecorativePrototype::autoplace::tile_restriction` - concept 'TileIDRestriction' accepts several incompatible value kinds; mapped to object  
  _(TileID|[TileID,TileID])_
* [runtime] `DecorativePrototype::autoplace::probability_expression` - concept 'NoiseExpression' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|double)_
* [runtime] `DecorativePrototype::autoplace::richness_expression` - concept 'NoiseExpression' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|double)_
* [runtime] `DecorativePrototype::autoplace::local_expressions` - concept 'NoiseExpression' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|double)_
* [runtime] `DecorativePrototype::stateless_visualisation::offset_x` - concept 'RangedValue' accepts several incompatible value kinds; mapped to object  
  _([float,float]|float)_
* [runtime] `DecorativePrototype::stateless_visualisation::offset_y` - concept 'RangedValue' accepts several incompatible value kinds; mapped to object  
  _([float,float]|float)_
* [runtime] `DecorativePrototype::stateless_visualisation::offset_z` - concept 'RangedValue' accepts several incompatible value kinds; mapped to object  
  _([float,float]|float)_
* [runtime] `DecorativePrototype::stateless_visualisation::speed_x` - concept 'RangedValue' accepts several incompatible value kinds; mapped to object  
  _([float,float]|float)_
* [runtime] `DecorativePrototype::stateless_visualisation::speed_y` - concept 'RangedValue' accepts several incompatible value kinds; mapped to object  
  _([float,float]|float)_
* [runtime] `DecorativePrototype::stateless_visualisation::speed_z` - concept 'RangedValue' accepts several incompatible value kinds; mapped to object  
  _([float,float]|float)_
* [runtime] `DecorativePrototype::stateless_visualisation::scale` - concept 'RangedValue' accepts several incompatible value kinds; mapped to object  
  _([float,float]|float)_
* [runtime] `EntityWithHealthPrototype::attack_reaction::action` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `ItemPrototype::spoil_to_trigger_result::trigger` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `NamedNoiseExpression::local_functions::expression` - concept 'NoiseExpression' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|double)_
* [runtime] `NamedNoiseExpression::local_functions::local_expressions` - concept 'NoiseExpression' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|double)_
* [runtime] `PlanetPrototype::map_gen_settings::property_expression_names` - scalar union mapped to object  
  _(string|boolean|double)_
* [runtime] `ProcessionPrototype::ground_timeline::layers` - concept 'ProcessionLayer' accepts several incompatible value kinds; mapped to object  
  _(PodDistanceTraveledProcessionLayer|PodMovementProcessionLayer|PodOpacityProcessionLayer|SingleGraphicProcessionLayer|CoverGraphicProcessionLayer|TintProcessionLayer|PodAnimationProcessionLayer)_
* [runtime] `Prototype::custom_tooltip_fields::name` - recursive concept 'LocalisedString' mapped to object
* [runtime] `Prototype::custom_tooltip_fields::name` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|array[LocalisedString])_
* [runtime] `Prototype::custom_tooltip_fields::value` - recursive concept 'LocalisedString' mapped to object
* [runtime] `Prototype::custom_tooltip_fields::value` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|array[LocalisedString])_
* [runtime] `Prototype::custom_tooltip_fields::quality_values` - recursive concept 'LocalisedString' mapped to object
* [runtime] `Prototype::custom_tooltip_fields::quality_values` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|array[LocalisedString])_
* [runtime] `SegmentPrototype::update_effects::effect` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `SpiderLegPrototype::lower_leg_dying_trigger_effects::effect` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `TechnologyPrototype::effects::effect_description` - recursive concept 'LocalisedString' mapped to object
* [runtime] `TechnologyPrototype::effects::effect_description` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|array[LocalisedString])_
* [runtime] `TechnologyPrototype::research_trigger::trigger_description` - recursive concept 'LocalisedString' mapped to object
* [runtime] `TechnologyPrototype::research_trigger::trigger_description` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|array[LocalisedString])_
* [runtime] `TipsAndTricksItem::skip_trigger::triggers` - concept 'TipTrigger' accepts several incompatible value kinds; mapped to object  
  _(OrTipTrigger|AndTipTrigger|SequenceTipTrigger|DependenciesMetTipTrigger|TimeElapsedTipTrigger|TimeSinceLastTipActivationTipTrigger|ResearchTechnologyTipTrigger|ResearchWithSciencePackTipTrigger|UnlockRecipeTipTrigger|CraftItemTipTrigger|BuildEntityTipTrigger|ManualTransferTipTrigger|ModuleTransferTipTrigger|StackTransferTipTrigger|EntityTransferTipTrigger|DropItemTipTrigger|SetRecipeTipTrigger|SetFilterTipTrigger|LimitChestTipTrigger|UsePipetteTipTrigger|SetLogisticRequestTipTrigger|UseConfirmTipTrigger|ToggleShowEntityInfoTipTrigger|GeneratingPowerTipTrigger|LowPowerTipTrigger|PasteEntitySettingsTipTrigger|FastReplaceTipTrigger|GroupAttackTipTrigger|FastBeltBendTipTrigger|BeltTraverseTipTrigger|PlaceEquipmentTipTrigger|ClearCursorTipTrigger|RotateEntityTipTrigger|FlipEntityTipTrigger|AlternativeBuildTipTrigger|GateOverRailBuildTipTrigger|ManualWireDragTipTrigger|ShootTipTrigger|ChangeSurfaceTipTrigger|ApplyStarterPackTipTrigger|MineItemByRobotTipTrigger|BuildEntityByRobotTipTrigger|PlanTrainPathTipTrigger|UseRailPlannerTipTrigger|ToggleRailLayerTipTrigger|EnterVehicleTipTrigger|SendSpidertronTipTrigger|ActivatePasteTipTrigger|KillTipTrigger)_
* [runtime] `TipsAndTricksItem::skip_trigger::triggers` - concept 'TipTrigger' accepts several incompatible value kinds; mapped to object  
  _(OrTipTrigger|AndTipTrigger|SequenceTipTrigger|DependenciesMetTipTrigger|TimeElapsedTipTrigger|TimeSinceLastTipActivationTipTrigger|ResearchTechnologyTipTrigger|ResearchWithSciencePackTipTrigger|UnlockRecipeTipTrigger|CraftItemTipTrigger|BuildEntityTipTrigger|ManualTransferTipTrigger|ModuleTransferTipTrigger|StackTransferTipTrigger|EntityTransferTipTrigger|DropItemTipTrigger|SetRecipeTipTrigger|SetFilterTipTrigger|LimitChestTipTrigger|UsePipetteTipTrigger|SetLogisticRequestTipTrigger|UseConfirmTipTrigger|ToggleShowEntityInfoTipTrigger|GeneratingPowerTipTrigger|LowPowerTipTrigger|PasteEntitySettingsTipTrigger|FastReplaceTipTrigger|GroupAttackTipTrigger|FastBeltBendTipTrigger|BeltTraverseTipTrigger|PlaceEquipmentTipTrigger|ClearCursorTipTrigger|RotateEntityTipTrigger|FlipEntityTipTrigger|AlternativeBuildTipTrigger|GateOverRailBuildTipTrigger|ManualWireDragTipTrigger|ShootTipTrigger|ChangeSurfaceTipTrigger|ApplyStarterPackTipTrigger|MineItemByRobotTipTrigger|BuildEntityByRobotTipTrigger|PlanTrainPathTipTrigger|UseRailPlannerTipTrigger|ToggleRailLayerTipTrigger|EnterVehicleTipTrigger|SendSpidertronTipTrigger|ActivatePasteTipTrigger|KillTipTrigger)_
* [runtime] `TipsAndTricksItem::skip_trigger::triggers` - concept 'TipTrigger' accepts several incompatible value kinds; mapped to object  
  _(OrTipTrigger|AndTipTrigger|SequenceTipTrigger|DependenciesMetTipTrigger|TimeElapsedTipTrigger|TimeSinceLastTipActivationTipTrigger|ResearchTechnologyTipTrigger|ResearchWithSciencePackTipTrigger|UnlockRecipeTipTrigger|CraftItemTipTrigger|BuildEntityTipTrigger|ManualTransferTipTrigger|ModuleTransferTipTrigger|StackTransferTipTrigger|EntityTransferTipTrigger|DropItemTipTrigger|SetRecipeTipTrigger|SetFilterTipTrigger|LimitChestTipTrigger|UsePipetteTipTrigger|SetLogisticRequestTipTrigger|UseConfirmTipTrigger|ToggleShowEntityInfoTipTrigger|GeneratingPowerTipTrigger|LowPowerTipTrigger|PasteEntitySettingsTipTrigger|FastReplaceTipTrigger|GroupAttackTipTrigger|FastBeltBendTipTrigger|BeltTraverseTipTrigger|PlaceEquipmentTipTrigger|ClearCursorTipTrigger|RotateEntityTipTrigger|FlipEntityTipTrigger|AlternativeBuildTipTrigger|GateOverRailBuildTipTrigger|ManualWireDragTipTrigger|ShootTipTrigger|ChangeSurfaceTipTrigger|ApplyStarterPackTipTrigger|MineItemByRobotTipTrigger|BuildEntityByRobotTipTrigger|PlanTrainPathTipTrigger|UseRailPlannerTipTrigger|ToggleRailLayerTipTrigger|EnterVehicleTipTrigger|SendSpidertronTipTrigger|ActivatePasteTipTrigger|KillTipTrigger)_
* [runtime] `UtilityConstants::color_filters::localised_name` - recursive concept 'LocalisedString' mapped to object
* [runtime] `UtilityConstants::color_filters::localised_name` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|array[LocalisedString])_
* [runtime] `AmbientSound::variable_sound::layers::sublayer_starting_offset` - concept 'RandomRange' accepts several incompatible value kinds; mapped to object  
  _([uint8,uint8]|uint8)_
* [runtime] `AmbientSound::variable_sound::layers::sublayer_offset` - concept 'RandomRange' accepts several incompatible value kinds; mapped to object  
  _([uint8,uint8]|uint8)_
* [runtime] `AmbientSound::variable_sound::layers::sample_length` - concept 'RandomRange' accepts several incompatible value kinds; mapped to object  
  _([uint8,uint8]|uint8)_
* [runtime] `AmbientSound::variable_sound::states::start_pause` - concept 'RandomRange' accepts several incompatible value kinds; mapped to object  
  _([uint8,uint8]|uint8)_
* [runtime] `AmbientSound::variable_sound::states::end_pause` - concept 'RandomRange' accepts several incompatible value kinds; mapped to object  
  _([uint8,uint8]|uint8)_
* [runtime] `AmbientSound::variable_sound::states::number_of_enabled_layers` - concept 'RandomRange' accepts several incompatible value kinds; mapped to object  
  _([uint8,uint8]|uint8)_
* [runtime] `PlanetPrototype::map_gen_settings::autoplace_controls::frequency` - concept 'MapGenSize' accepts several incompatible value kinds; mapped to object  
  _(float|literal(string:none)|literal(string:very-low)|literal(string:very-small)|literal(string:very-poor)|literal(string:low)|literal(string:small)|literal(string:poor)|literal(string:normal)|literal(string:medium)|literal(string:regular)|literal(string:high)|literal(string:big)|literal(string:good)|literal(string:very-high)|literal(string:very-big)|literal(string:very-good))_
* [runtime] `PlanetPrototype::map_gen_settings::autoplace_controls::size` - concept 'MapGenSize' accepts several incompatible value kinds; mapped to object  
  _(float|literal(string:none)|literal(string:very-low)|literal(string:very-small)|literal(string:very-poor)|literal(string:low)|literal(string:small)|literal(string:poor)|literal(string:normal)|literal(string:medium)|literal(string:regular)|literal(string:high)|literal(string:big)|literal(string:good)|literal(string:very-high)|literal(string:very-big)|literal(string:very-good))_
* [runtime] `PlanetPrototype::map_gen_settings::autoplace_controls::richness` - concept 'MapGenSize' accepts several incompatible value kinds; mapped to object  
  _(float|literal(string:none)|literal(string:very-low)|literal(string:very-small)|literal(string:very-poor)|literal(string:low)|literal(string:small)|literal(string:poor)|literal(string:normal)|literal(string:medium)|literal(string:regular)|literal(string:high)|literal(string:big)|literal(string:good)|literal(string:very-high)|literal(string:very-big)|literal(string:very-good))_
* [runtime] `SpiderUnitPrototype::spider_engine::legs::leg_hit_the_ground_trigger` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `SpiderUnitPrototype::spider_engine::legs::leg_hit_the_ground_when_attacking_trigger` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `AmmoItemPrototype::ammo_type::action` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `ArtilleryFlarePrototype::ended_in_water_trigger_effect::non_colliding_fail_result` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `ArtilleryFlarePrototype::ended_in_water_trigger_effect::action` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `ArtilleryProjectilePrototype::action::range_effects` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `AsteroidChunkPrototype::minable::results` - concept 'ProductPrototype' accepts several incompatible value kinds; mapped to object  
  _(ItemProductPrototype|FluidProductPrototype)_
* [runtime] `AsteroidChunkPrototype::minable::mining_trigger` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `BlueprintItemPrototype::alt_select::mode` - concept 'SelectionModeFlags' accepts several incompatible value kinds; mapped to object  
  _((literal(string:blueprint)|literal(string:deconstruct)|literal(string:cancel-deconstruct)|literal(string:items)|literal(string:trees)|literal(string:buildable-type)|literal(string:nothing)|literal(string:items-to-place)|literal(string:any-entity)|literal(string:any-tile)|literal(string:same-force)|literal(string:not-same-force)|literal(string:friend)|literal(string:enemy)|literal(string:upgrade)|literal(string:cancel-upgrade)|literal(string:downgrade)|literal(string:entity-with-health)|literal(string:is-military-target)|literal(string:entity-with-owner)|literal(string:avoid-rolling-stock)|literal(string:avoid-vehicle)|literal(string:controllable)|literal(string:controllable-add)|literal(string:controllable-remove)|literal(string:entity-ghost)|literal(string:tile-ghost))|array[(literal(string:blueprint)|literal(string:deconstruct)|literal(string:cancel-deconstruct)|literal(string:items)|literal(string:trees)|literal(string:buildable-type)|literal(string:nothing)|literal(string:items-to-place)|literal(string:any-entity)|literal(string:any-tile)|literal(string:same-force)|literal(string:not-same-force)|literal(string:friend)|literal(string:enemy)|literal(string:upgrade)|literal(string:cancel-upgrade)|literal(string:downgrade)|literal(string:entity-with-health)|literal(string:is-military-target)|literal(string:entity-with-owner)|literal(string:avoid-rolling-stock)|literal(string:avoid-vehicle)|literal(string:controllable)|literal(string:controllable-add)|literal(string:controllable-remove)|literal(string:entity-ghost)|literal(string:tile-ghost))])_
* [runtime] `CapsulePrototype::capsule_action::attack_parameters` - concept 'AttackParameters' accepts several incompatible value kinds; mapped to object  
  _(ProjectileAttackParameters|BeamAttackParameters|StreamAttackParameters)_
* [runtime] `CapsulePrototype::capsule_action::attack_parameters` - concept 'AttackParameters' accepts several incompatible value kinds; mapped to object  
  _(ProjectileAttackParameters|BeamAttackParameters|StreamAttackParameters)_
* [runtime] `CapsulePrototype::capsule_action::attack_parameters` - concept 'AttackParameters' accepts several incompatible value kinds; mapped to object  
  _(ProjectileAttackParameters|BeamAttackParameters|StreamAttackParameters)_
* [runtime] `DecorativePrototype::autoplace::tile_restriction` - concept 'TileIDRestriction' accepts several incompatible value kinds; mapped to object  
  _(TileID|[TileID,TileID])_
* [runtime] `DecorativePrototype::autoplace::probability_expression` - concept 'NoiseExpression' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|double)_
* [runtime] `DecorativePrototype::autoplace::richness_expression` - concept 'NoiseExpression' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|double)_
* [runtime] `DecorativePrototype::autoplace::local_expressions` - concept 'NoiseExpression' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|double)_
* [runtime] `DecorativePrototype::stateless_visualisation::offset_x` - concept 'RangedValue' accepts several incompatible value kinds; mapped to object  
  _([float,float]|float)_
* [runtime] `DecorativePrototype::stateless_visualisation::offset_y` - concept 'RangedValue' accepts several incompatible value kinds; mapped to object  
  _([float,float]|float)_
* [runtime] `DecorativePrototype::stateless_visualisation::offset_z` - concept 'RangedValue' accepts several incompatible value kinds; mapped to object  
  _([float,float]|float)_
* [runtime] `DecorativePrototype::stateless_visualisation::speed_x` - concept 'RangedValue' accepts several incompatible value kinds; mapped to object  
  _([float,float]|float)_
* [runtime] `DecorativePrototype::stateless_visualisation::speed_y` - concept 'RangedValue' accepts several incompatible value kinds; mapped to object  
  _([float,float]|float)_
* [runtime] `DecorativePrototype::stateless_visualisation::speed_z` - concept 'RangedValue' accepts several incompatible value kinds; mapped to object  
  _([float,float]|float)_
* [runtime] `DecorativePrototype::stateless_visualisation::scale` - concept 'RangedValue' accepts several incompatible value kinds; mapped to object  
  _([float,float]|float)_
* [runtime] `EntityWithHealthPrototype::attack_reaction::action` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `ItemPrototype::spoil_to_trigger_result::trigger` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `NamedNoiseExpression::local_functions::expression` - concept 'NoiseExpression' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|double)_
* [runtime] `NamedNoiseExpression::local_functions::local_expressions` - concept 'NoiseExpression' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|double)_
* [runtime] `PlanetPrototype::map_gen_settings::property_expression_names` - scalar union mapped to object  
  _(string|boolean|double)_
* [runtime] `ProcessionPrototype::ground_timeline::layers` - concept 'ProcessionLayer' accepts several incompatible value kinds; mapped to object  
  _(PodDistanceTraveledProcessionLayer|PodMovementProcessionLayer|PodOpacityProcessionLayer|SingleGraphicProcessionLayer|CoverGraphicProcessionLayer|TintProcessionLayer|PodAnimationProcessionLayer)_
* [runtime] `Prototype::custom_tooltip_fields::name` - recursive concept 'LocalisedString' mapped to object
* [runtime] `Prototype::custom_tooltip_fields::name` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|array[LocalisedString])_
* [runtime] `Prototype::custom_tooltip_fields::value` - recursive concept 'LocalisedString' mapped to object
* [runtime] `Prototype::custom_tooltip_fields::value` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|array[LocalisedString])_
* [runtime] `Prototype::custom_tooltip_fields::quality_values` - recursive concept 'LocalisedString' mapped to object
* [runtime] `Prototype::custom_tooltip_fields::quality_values` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|array[LocalisedString])_
* [runtime] `SegmentPrototype::update_effects::effect` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `SpiderLegPrototype::lower_leg_dying_trigger_effects::effect` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `TechnologyPrototype::effects::effect_description` - recursive concept 'LocalisedString' mapped to object
* [runtime] `TechnologyPrototype::effects::effect_description` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|array[LocalisedString])_
* [runtime] `TechnologyPrototype::research_trigger::trigger_description` - recursive concept 'LocalisedString' mapped to object
* [runtime] `TechnologyPrototype::research_trigger::trigger_description` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|array[LocalisedString])_
* [runtime] `TipsAndTricksItem::skip_trigger::triggers` - concept 'TipTrigger' accepts several incompatible value kinds; mapped to object  
  _(OrTipTrigger|AndTipTrigger|SequenceTipTrigger|DependenciesMetTipTrigger|TimeElapsedTipTrigger|TimeSinceLastTipActivationTipTrigger|ResearchTechnologyTipTrigger|ResearchWithSciencePackTipTrigger|UnlockRecipeTipTrigger|CraftItemTipTrigger|BuildEntityTipTrigger|ManualTransferTipTrigger|ModuleTransferTipTrigger|StackTransferTipTrigger|EntityTransferTipTrigger|DropItemTipTrigger|SetRecipeTipTrigger|SetFilterTipTrigger|LimitChestTipTrigger|UsePipetteTipTrigger|SetLogisticRequestTipTrigger|UseConfirmTipTrigger|ToggleShowEntityInfoTipTrigger|GeneratingPowerTipTrigger|LowPowerTipTrigger|PasteEntitySettingsTipTrigger|FastReplaceTipTrigger|GroupAttackTipTrigger|FastBeltBendTipTrigger|BeltTraverseTipTrigger|PlaceEquipmentTipTrigger|ClearCursorTipTrigger|RotateEntityTipTrigger|FlipEntityTipTrigger|AlternativeBuildTipTrigger|GateOverRailBuildTipTrigger|ManualWireDragTipTrigger|ShootTipTrigger|ChangeSurfaceTipTrigger|ApplyStarterPackTipTrigger|MineItemByRobotTipTrigger|BuildEntityByRobotTipTrigger|PlanTrainPathTipTrigger|UseRailPlannerTipTrigger|ToggleRailLayerTipTrigger|EnterVehicleTipTrigger|SendSpidertronTipTrigger|ActivatePasteTipTrigger|KillTipTrigger)_
* [runtime] `TipsAndTricksItem::skip_trigger::triggers` - concept 'TipTrigger' accepts several incompatible value kinds; mapped to object  
  _(OrTipTrigger|AndTipTrigger|SequenceTipTrigger|DependenciesMetTipTrigger|TimeElapsedTipTrigger|TimeSinceLastTipActivationTipTrigger|ResearchTechnologyTipTrigger|ResearchWithSciencePackTipTrigger|UnlockRecipeTipTrigger|CraftItemTipTrigger|BuildEntityTipTrigger|ManualTransferTipTrigger|ModuleTransferTipTrigger|StackTransferTipTrigger|EntityTransferTipTrigger|DropItemTipTrigger|SetRecipeTipTrigger|SetFilterTipTrigger|LimitChestTipTrigger|UsePipetteTipTrigger|SetLogisticRequestTipTrigger|UseConfirmTipTrigger|ToggleShowEntityInfoTipTrigger|GeneratingPowerTipTrigger|LowPowerTipTrigger|PasteEntitySettingsTipTrigger|FastReplaceTipTrigger|GroupAttackTipTrigger|FastBeltBendTipTrigger|BeltTraverseTipTrigger|PlaceEquipmentTipTrigger|ClearCursorTipTrigger|RotateEntityTipTrigger|FlipEntityTipTrigger|AlternativeBuildTipTrigger|GateOverRailBuildTipTrigger|ManualWireDragTipTrigger|ShootTipTrigger|ChangeSurfaceTipTrigger|ApplyStarterPackTipTrigger|MineItemByRobotTipTrigger|BuildEntityByRobotTipTrigger|PlanTrainPathTipTrigger|UseRailPlannerTipTrigger|ToggleRailLayerTipTrigger|EnterVehicleTipTrigger|SendSpidertronTipTrigger|ActivatePasteTipTrigger|KillTipTrigger)_
* [runtime] `TipsAndTricksItem::skip_trigger::triggers` - concept 'TipTrigger' accepts several incompatible value kinds; mapped to object  
  _(OrTipTrigger|AndTipTrigger|SequenceTipTrigger|DependenciesMetTipTrigger|TimeElapsedTipTrigger|TimeSinceLastTipActivationTipTrigger|ResearchTechnologyTipTrigger|ResearchWithSciencePackTipTrigger|UnlockRecipeTipTrigger|CraftItemTipTrigger|BuildEntityTipTrigger|ManualTransferTipTrigger|ModuleTransferTipTrigger|StackTransferTipTrigger|EntityTransferTipTrigger|DropItemTipTrigger|SetRecipeTipTrigger|SetFilterTipTrigger|LimitChestTipTrigger|UsePipetteTipTrigger|SetLogisticRequestTipTrigger|UseConfirmTipTrigger|ToggleShowEntityInfoTipTrigger|GeneratingPowerTipTrigger|LowPowerTipTrigger|PasteEntitySettingsTipTrigger|FastReplaceTipTrigger|GroupAttackTipTrigger|FastBeltBendTipTrigger|BeltTraverseTipTrigger|PlaceEquipmentTipTrigger|ClearCursorTipTrigger|RotateEntityTipTrigger|FlipEntityTipTrigger|AlternativeBuildTipTrigger|GateOverRailBuildTipTrigger|ManualWireDragTipTrigger|ShootTipTrigger|ChangeSurfaceTipTrigger|ApplyStarterPackTipTrigger|MineItemByRobotTipTrigger|BuildEntityByRobotTipTrigger|PlanTrainPathTipTrigger|UseRailPlannerTipTrigger|ToggleRailLayerTipTrigger|EnterVehicleTipTrigger|SendSpidertronTipTrigger|ActivatePasteTipTrigger|KillTipTrigger)_
* [runtime] `UtilityConstants::color_filters::localised_name` - recursive concept 'LocalisedString' mapped to object
* [runtime] `UtilityConstants::color_filters::localised_name` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|array[LocalisedString])_
* [runtime] `AmbientSound::variable_sound::layers::sublayer_starting_offset` - concept 'RandomRange' accepts several incompatible value kinds; mapped to object  
  _([uint8,uint8]|uint8)_
* [runtime] `AmbientSound::variable_sound::layers::sublayer_offset` - concept 'RandomRange' accepts several incompatible value kinds; mapped to object  
  _([uint8,uint8]|uint8)_
* [runtime] `AmbientSound::variable_sound::layers::sample_length` - concept 'RandomRange' accepts several incompatible value kinds; mapped to object  
  _([uint8,uint8]|uint8)_
* [runtime] `AmbientSound::variable_sound::states::start_pause` - concept 'RandomRange' accepts several incompatible value kinds; mapped to object  
  _([uint8,uint8]|uint8)_
* [runtime] `AmbientSound::variable_sound::states::end_pause` - concept 'RandomRange' accepts several incompatible value kinds; mapped to object  
  _([uint8,uint8]|uint8)_
* [runtime] `AmbientSound::variable_sound::states::number_of_enabled_layers` - concept 'RandomRange' accepts several incompatible value kinds; mapped to object  
  _([uint8,uint8]|uint8)_
* [runtime] `PlanetPrototype::map_gen_settings::autoplace_controls::frequency` - concept 'MapGenSize' accepts several incompatible value kinds; mapped to object  
  _(float|literal(string:none)|literal(string:very-low)|literal(string:very-small)|literal(string:very-poor)|literal(string:low)|literal(string:small)|literal(string:poor)|literal(string:normal)|literal(string:medium)|literal(string:regular)|literal(string:high)|literal(string:big)|literal(string:good)|literal(string:very-high)|literal(string:very-big)|literal(string:very-good))_
* [runtime] `PlanetPrototype::map_gen_settings::autoplace_controls::size` - concept 'MapGenSize' accepts several incompatible value kinds; mapped to object  
  _(float|literal(string:none)|literal(string:very-low)|literal(string:very-small)|literal(string:very-poor)|literal(string:low)|literal(string:small)|literal(string:poor)|literal(string:normal)|literal(string:medium)|literal(string:regular)|literal(string:high)|literal(string:big)|literal(string:good)|literal(string:very-high)|literal(string:very-big)|literal(string:very-good))_
* [runtime] `PlanetPrototype::map_gen_settings::autoplace_controls::richness` - concept 'MapGenSize' accepts several incompatible value kinds; mapped to object  
  _(float|literal(string:none)|literal(string:very-low)|literal(string:very-small)|literal(string:very-poor)|literal(string:low)|literal(string:small)|literal(string:poor)|literal(string:normal)|literal(string:medium)|literal(string:regular)|literal(string:high)|literal(string:big)|literal(string:good)|literal(string:very-high)|literal(string:very-big)|literal(string:very-good))_
* [runtime] `SpiderUnitPrototype::spider_engine::legs::leg_hit_the_ground_trigger` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `SpiderUnitPrototype::spider_engine::legs::leg_hit_the_ground_when_attacking_trigger` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `AmbientSound::variable_sound::states::layers_properties::sequence_length` - concept 'RandomRange' accepts several incompatible value kinds; mapped to object  
  _([uint8,uint8]|uint8)_
* [runtime] `AmbientSound::variable_sound::states::layers_properties::number_of_repetitions` - concept 'RandomRange' accepts several incompatible value kinds; mapped to object  
  _([uint8,uint8]|uint8)_
* [runtime] `AmbientSound::variable_sound::states::layers_properties::start_pause` - concept 'RandomRange' accepts several incompatible value kinds; mapped to object  
  _([uint8,uint8]|uint8)_
* [runtime] `AmbientSound::variable_sound::states::layers_properties::pause_between_samples` - concept 'RandomRange' accepts several incompatible value kinds; mapped to object  
  _([uint8,uint8]|uint8)_
* [runtime] `AmbientSound::variable_sound::states::layers_properties::pause_between_repetitions` - concept 'RandomRange' accepts several incompatible value kinds; mapped to object  
  _([uint8,uint8]|uint8)_
* [runtime] `AmbientSound::variable_sound::states::layers_properties::end_pause` - concept 'RandomRange' accepts several incompatible value kinds; mapped to object  
  _([uint8,uint8]|uint8)_
* [runtime] `AmmoItemPrototype::ammo_type::action` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `ArtilleryFlarePrototype::ended_in_water_trigger_effect::non_colliding_fail_result` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `ArtilleryFlarePrototype::ended_in_water_trigger_effect::action` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `ArtilleryProjectilePrototype::action::range_effects` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `AsteroidChunkPrototype::minable::results` - concept 'ProductPrototype' accepts several incompatible value kinds; mapped to object  
  _(ItemProductPrototype|FluidProductPrototype)_
* [runtime] `AsteroidChunkPrototype::minable::mining_trigger` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `BlueprintItemPrototype::alt_select::mode` - concept 'SelectionModeFlags' accepts several incompatible value kinds; mapped to object  
  _((literal(string:blueprint)|literal(string:deconstruct)|literal(string:cancel-deconstruct)|literal(string:items)|literal(string:trees)|literal(string:buildable-type)|literal(string:nothing)|literal(string:items-to-place)|literal(string:any-entity)|literal(string:any-tile)|literal(string:same-force)|literal(string:not-same-force)|literal(string:friend)|literal(string:enemy)|literal(string:upgrade)|literal(string:cancel-upgrade)|literal(string:downgrade)|literal(string:entity-with-health)|literal(string:is-military-target)|literal(string:entity-with-owner)|literal(string:avoid-rolling-stock)|literal(string:avoid-vehicle)|literal(string:controllable)|literal(string:controllable-add)|literal(string:controllable-remove)|literal(string:entity-ghost)|literal(string:tile-ghost))|array[(literal(string:blueprint)|literal(string:deconstruct)|literal(string:cancel-deconstruct)|literal(string:items)|literal(string:trees)|literal(string:buildable-type)|literal(string:nothing)|literal(string:items-to-place)|literal(string:any-entity)|literal(string:any-tile)|literal(string:same-force)|literal(string:not-same-force)|literal(string:friend)|literal(string:enemy)|literal(string:upgrade)|literal(string:cancel-upgrade)|literal(string:downgrade)|literal(string:entity-with-health)|literal(string:is-military-target)|literal(string:entity-with-owner)|literal(string:avoid-rolling-stock)|literal(string:avoid-vehicle)|literal(string:controllable)|literal(string:controllable-add)|literal(string:controllable-remove)|literal(string:entity-ghost)|literal(string:tile-ghost))])_
* [runtime] `CapsulePrototype::capsule_action::attack_parameters` - concept 'AttackParameters' accepts several incompatible value kinds; mapped to object  
  _(ProjectileAttackParameters|BeamAttackParameters|StreamAttackParameters)_
* [runtime] `CapsulePrototype::capsule_action::attack_parameters` - concept 'AttackParameters' accepts several incompatible value kinds; mapped to object  
  _(ProjectileAttackParameters|BeamAttackParameters|StreamAttackParameters)_
* [runtime] `CapsulePrototype::capsule_action::attack_parameters` - concept 'AttackParameters' accepts several incompatible value kinds; mapped to object  
  _(ProjectileAttackParameters|BeamAttackParameters|StreamAttackParameters)_
* [runtime] `DecorativePrototype::autoplace::tile_restriction` - concept 'TileIDRestriction' accepts several incompatible value kinds; mapped to object  
  _(TileID|[TileID,TileID])_
* [runtime] `DecorativePrototype::autoplace::probability_expression` - concept 'NoiseExpression' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|double)_
* [runtime] `DecorativePrototype::autoplace::richness_expression` - concept 'NoiseExpression' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|double)_
* [runtime] `DecorativePrototype::autoplace::local_expressions` - concept 'NoiseExpression' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|double)_
* [runtime] `DecorativePrototype::stateless_visualisation::offset_x` - concept 'RangedValue' accepts several incompatible value kinds; mapped to object  
  _([float,float]|float)_
* [runtime] `DecorativePrototype::stateless_visualisation::offset_y` - concept 'RangedValue' accepts several incompatible value kinds; mapped to object  
  _([float,float]|float)_
* [runtime] `DecorativePrototype::stateless_visualisation::offset_z` - concept 'RangedValue' accepts several incompatible value kinds; mapped to object  
  _([float,float]|float)_
* [runtime] `DecorativePrototype::stateless_visualisation::speed_x` - concept 'RangedValue' accepts several incompatible value kinds; mapped to object  
  _([float,float]|float)_
* [runtime] `DecorativePrototype::stateless_visualisation::speed_y` - concept 'RangedValue' accepts several incompatible value kinds; mapped to object  
  _([float,float]|float)_
* [runtime] `DecorativePrototype::stateless_visualisation::speed_z` - concept 'RangedValue' accepts several incompatible value kinds; mapped to object  
  _([float,float]|float)_
* [runtime] `DecorativePrototype::stateless_visualisation::scale` - concept 'RangedValue' accepts several incompatible value kinds; mapped to object  
  _([float,float]|float)_
* [runtime] `EntityWithHealthPrototype::attack_reaction::action` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `ItemPrototype::spoil_to_trigger_result::trigger` - concept 'Trigger' accepts several incompatible value kinds; mapped to object  
  _((DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)|array[(DirectTriggerItem|AreaTriggerItem|LineTriggerItem|ClusterTriggerItem)])_
* [runtime] `NamedNoiseExpression::local_functions::expression` - concept 'NoiseExpression' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|double)_
* [runtime] `NamedNoiseExpression::local_functions::local_expressions` - concept 'NoiseExpression' accepts several incompatible value kinds; mapped to object  
  _(string|boolean|double)_
* [runtime] `PlanetPrototype::map_gen_settings::property_expression_names` - scalar union mapped to object  
  _(string|boolean|double)_
* [runtime] `ProcessionPrototype::ground_timeline::layers` - concept 'ProcessionLayer' accepts several incompatible value kinds; mapped to object  
  _(PodDistanceTraveledProcessionLayer|PodMovementProcessionLayer|PodOpacityProcessionLayer|SingleGraphicProcessionLayer|CoverGraphicProcessionLayer|TintProcessionLayer|PodAnimationProcessionLayer)_
* [runtime] `Prototype::custom_tooltip_fields::name` - recursive concept 'LocalisedString' mapped to object
* [runtime] `Prototype::custom_tooltip_fields::name` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|array[LocalisedString])_
* [runtime] `Prototype::custom_tooltip_fields::value` - recursive concept 'LocalisedString' mapped to object
* [runtime] `Prototype::custom_tooltip_fields::value` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|array[LocalisedString])_
* [runtime] `Prototype::custom_tooltip_fields::quality_values` - recursive concept 'LocalisedString' mapped to object
* [runtime] `Prototype::custom_tooltip_fields::quality_values` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|array[LocalisedString])_
* [runtime] `SegmentPrototype::update_effects::effect` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `SpiderLegPrototype::lower_leg_dying_trigger_effects::effect` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `TechnologyPrototype::effects::effect_description` - recursive concept 'LocalisedString' mapped to object
* [runtime] `TechnologyPrototype::effects::effect_description` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|array[LocalisedString])_
* [runtime] `TechnologyPrototype::research_trigger::trigger_description` - recursive concept 'LocalisedString' mapped to object
* [runtime] `TechnologyPrototype::research_trigger::trigger_description` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|array[LocalisedString])_
* [runtime] `TipsAndTricksItem::skip_trigger::triggers` - concept 'TipTrigger' accepts several incompatible value kinds; mapped to object  
  _(OrTipTrigger|AndTipTrigger|SequenceTipTrigger|DependenciesMetTipTrigger|TimeElapsedTipTrigger|TimeSinceLastTipActivationTipTrigger|ResearchTechnologyTipTrigger|ResearchWithSciencePackTipTrigger|UnlockRecipeTipTrigger|CraftItemTipTrigger|BuildEntityTipTrigger|ManualTransferTipTrigger|ModuleTransferTipTrigger|StackTransferTipTrigger|EntityTransferTipTrigger|DropItemTipTrigger|SetRecipeTipTrigger|SetFilterTipTrigger|LimitChestTipTrigger|UsePipetteTipTrigger|SetLogisticRequestTipTrigger|UseConfirmTipTrigger|ToggleShowEntityInfoTipTrigger|GeneratingPowerTipTrigger|LowPowerTipTrigger|PasteEntitySettingsTipTrigger|FastReplaceTipTrigger|GroupAttackTipTrigger|FastBeltBendTipTrigger|BeltTraverseTipTrigger|PlaceEquipmentTipTrigger|ClearCursorTipTrigger|RotateEntityTipTrigger|FlipEntityTipTrigger|AlternativeBuildTipTrigger|GateOverRailBuildTipTrigger|ManualWireDragTipTrigger|ShootTipTrigger|ChangeSurfaceTipTrigger|ApplyStarterPackTipTrigger|MineItemByRobotTipTrigger|BuildEntityByRobotTipTrigger|PlanTrainPathTipTrigger|UseRailPlannerTipTrigger|ToggleRailLayerTipTrigger|EnterVehicleTipTrigger|SendSpidertronTipTrigger|ActivatePasteTipTrigger|KillTipTrigger)_
* [runtime] `TipsAndTricksItem::skip_trigger::triggers` - concept 'TipTrigger' accepts several incompatible value kinds; mapped to object  
  _(OrTipTrigger|AndTipTrigger|SequenceTipTrigger|DependenciesMetTipTrigger|TimeElapsedTipTrigger|TimeSinceLastTipActivationTipTrigger|ResearchTechnologyTipTrigger|ResearchWithSciencePackTipTrigger|UnlockRecipeTipTrigger|CraftItemTipTrigger|BuildEntityTipTrigger|ManualTransferTipTrigger|ModuleTransferTipTrigger|StackTransferTipTrigger|EntityTransferTipTrigger|DropItemTipTrigger|SetRecipeTipTrigger|SetFilterTipTrigger|LimitChestTipTrigger|UsePipetteTipTrigger|SetLogisticRequestTipTrigger|UseConfirmTipTrigger|ToggleShowEntityInfoTipTrigger|GeneratingPowerTipTrigger|LowPowerTipTrigger|PasteEntitySettingsTipTrigger|FastReplaceTipTrigger|GroupAttackTipTrigger|FastBeltBendTipTrigger|BeltTraverseTipTrigger|PlaceEquipmentTipTrigger|ClearCursorTipTrigger|RotateEntityTipTrigger|FlipEntityTipTrigger|AlternativeBuildTipTrigger|GateOverRailBuildTipTrigger|ManualWireDragTipTrigger|ShootTipTrigger|ChangeSurfaceTipTrigger|ApplyStarterPackTipTrigger|MineItemByRobotTipTrigger|BuildEntityByRobotTipTrigger|PlanTrainPathTipTrigger|UseRailPlannerTipTrigger|ToggleRailLayerTipTrigger|EnterVehicleTipTrigger|SendSpidertronTipTrigger|ActivatePasteTipTrigger|KillTipTrigger)_
* [runtime] `TipsAndTricksItem::skip_trigger::triggers` - concept 'TipTrigger' accepts several incompatible value kinds; mapped to object  
  _(OrTipTrigger|AndTipTrigger|SequenceTipTrigger|DependenciesMetTipTrigger|TimeElapsedTipTrigger|TimeSinceLastTipActivationTipTrigger|ResearchTechnologyTipTrigger|ResearchWithSciencePackTipTrigger|UnlockRecipeTipTrigger|CraftItemTipTrigger|BuildEntityTipTrigger|ManualTransferTipTrigger|ModuleTransferTipTrigger|StackTransferTipTrigger|EntityTransferTipTrigger|DropItemTipTrigger|SetRecipeTipTrigger|SetFilterTipTrigger|LimitChestTipTrigger|UsePipetteTipTrigger|SetLogisticRequestTipTrigger|UseConfirmTipTrigger|ToggleShowEntityInfoTipTrigger|GeneratingPowerTipTrigger|LowPowerTipTrigger|PasteEntitySettingsTipTrigger|FastReplaceTipTrigger|GroupAttackTipTrigger|FastBeltBendTipTrigger|BeltTraverseTipTrigger|PlaceEquipmentTipTrigger|ClearCursorTipTrigger|RotateEntityTipTrigger|FlipEntityTipTrigger|AlternativeBuildTipTrigger|GateOverRailBuildTipTrigger|ManualWireDragTipTrigger|ShootTipTrigger|ChangeSurfaceTipTrigger|ApplyStarterPackTipTrigger|MineItemByRobotTipTrigger|BuildEntityByRobotTipTrigger|PlanTrainPathTipTrigger|UseRailPlannerTipTrigger|ToggleRailLayerTipTrigger|EnterVehicleTipTrigger|SendSpidertronTipTrigger|ActivatePasteTipTrigger|KillTipTrigger)_
* [runtime] `UtilityConstants::color_filters::localised_name` - recursive concept 'LocalisedString' mapped to object
* [runtime] `UtilityConstants::color_filters::localised_name` - concept 'LocalisedString' accepts several incompatible value kinds; mapped to object  
  _(string|array[LocalisedString])_
* [runtime] `AmbientSound::variable_sound::layers::sublayer_starting_offset` - concept 'RandomRange' accepts several incompatible value kinds; mapped to object  
  _([uint8,uint8]|uint8)_
* [runtime] `AmbientSound::variable_sound::layers::sublayer_offset` - concept 'RandomRange' accepts several incompatible value kinds; mapped to object  
  _([uint8,uint8]|uint8)_
* [runtime] `AmbientSound::variable_sound::layers::sample_length` - concept 'RandomRange' accepts several incompatible value kinds; mapped to object  
  _([uint8,uint8]|uint8)_
* [runtime] `AmbientSound::variable_sound::states::start_pause` - concept 'RandomRange' accepts several incompatible value kinds; mapped to object  
  _([uint8,uint8]|uint8)_
* [runtime] `AmbientSound::variable_sound::states::end_pause` - concept 'RandomRange' accepts several incompatible value kinds; mapped to object  
  _([uint8,uint8]|uint8)_
* [runtime] `AmbientSound::variable_sound::states::number_of_enabled_layers` - concept 'RandomRange' accepts several incompatible value kinds; mapped to object  
  _([uint8,uint8]|uint8)_
* [runtime] `PlanetPrototype::map_gen_settings::autoplace_controls::frequency` - concept 'MapGenSize' accepts several incompatible value kinds; mapped to object  
  _(float|literal(string:none)|literal(string:very-low)|literal(string:very-small)|literal(string:very-poor)|literal(string:low)|literal(string:small)|literal(string:poor)|literal(string:normal)|literal(string:medium)|literal(string:regular)|literal(string:high)|literal(string:big)|literal(string:good)|literal(string:very-high)|literal(string:very-big)|literal(string:very-good))_
* [runtime] `PlanetPrototype::map_gen_settings::autoplace_controls::size` - concept 'MapGenSize' accepts several incompatible value kinds; mapped to object  
  _(float|literal(string:none)|literal(string:very-low)|literal(string:very-small)|literal(string:very-poor)|literal(string:low)|literal(string:small)|literal(string:poor)|literal(string:normal)|literal(string:medium)|literal(string:regular)|literal(string:high)|literal(string:big)|literal(string:good)|literal(string:very-high)|literal(string:very-big)|literal(string:very-good))_
* [runtime] `PlanetPrototype::map_gen_settings::autoplace_controls::richness` - concept 'MapGenSize' accepts several incompatible value kinds; mapped to object  
  _(float|literal(string:none)|literal(string:very-low)|literal(string:very-small)|literal(string:very-poor)|literal(string:low)|literal(string:small)|literal(string:poor)|literal(string:normal)|literal(string:medium)|literal(string:regular)|literal(string:high)|literal(string:big)|literal(string:good)|literal(string:very-high)|literal(string:very-big)|literal(string:very-good))_
* [runtime] `SpiderUnitPrototype::spider_engine::legs::leg_hit_the_ground_trigger` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `SpiderUnitPrototype::spider_engine::legs::leg_hit_the_ground_when_attacking_trigger` - concept 'TriggerEffect' accepts several incompatible value kinds; mapped to object  
  _((DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)|array[(DamageEntityTriggerEffectItem|DamageTileTriggerEffectItem|CreateEntityTriggerEffectItem|CreateExplosionTriggerEffectItem|CreateFireTriggerEffectItem|CreateSmokeTriggerEffectItem|CreateTrivialSmokeEffectItem|CreateAsteroidChunkEffectItem|CreateParticleTriggerEffectItem|CreateStickerTriggerEffectItem|CreateDecorativesTriggerEffectItem|NestedTriggerEffectItem|PlaySoundTriggerEffectItem|PushBackTriggerEffectItem|DestroyCliffsTriggerEffectItem|ShowExplosionOnChartTriggerEffectItem|InsertItemTriggerEffectItem|ScriptTriggerEffectItem|SetTileTriggerEffectItem|InvokeTileEffectTriggerEffectItem|DestroyDecorativesTriggerEffectItem|CameraEffectTriggerEffectItem|ActivateImpactTriggerEffectItem|CreatePollutionTriggerEffectItem)])_
* [runtime] `AmbientSound::variable_sound::states::layers_properties::sequence_length` - concept 'RandomRange' accepts several incompatible value kinds; mapped to object  
  _([uint8,uint8]|uint8)_
* [runtime] `AmbientSound::variable_sound::states::layers_properties::number_of_repetitions` - concept 'RandomRange' accepts several incompatible value kinds; mapped to object  
  _([uint8,uint8]|uint8)_
* [runtime] `AmbientSound::variable_sound::states::layers_properties::start_pause` - concept 'RandomRange' accepts several incompatible value kinds; mapped to object  
  _([uint8,uint8]|uint8)_
* [runtime] `AmbientSound::variable_sound::states::layers_properties::pause_between_samples` - concept 'RandomRange' accepts several incompatible value kinds; mapped to object  
  _([uint8,uint8]|uint8)_
* [runtime] `AmbientSound::variable_sound::states::layers_properties::pause_between_repetitions` - concept 'RandomRange' accepts several incompatible value kinds; mapped to object  
  _([uint8,uint8]|uint8)_
* [runtime] `AmbientSound::variable_sound::states::layers_properties::end_pause` - concept 'RandomRange' accepts several incompatible value kinds; mapped to object  
  _([uint8,uint8]|uint8)_
* [prototype] `data-stage` - prototype tables are emitted as typed views; building a prototype table from C# is not generated  
  _Use the raw data table or a hand written helper (docs/LIMITATIONS.md)_

