Logic of working a systems is very simple. The project have two systems, first one "SpeedSystem" - copy value (100f) from "Speed" component to "MoveSpeed" component. Second system "ModifySpeedSystem" just multiply "MoveSpeed" component value (100f) by self value (0f). The order of execution of these systems is strictly specified - "SpeedSystem" calls before "MoveForwardSystem" and "ModifySpeedSystem" calls before "MoveForwardSystem" but after "SpeedSystem".

If you open SampleScene from the project, you can find two game objects, called "IgnoreSystemsOrder_Spawner" and "SuccessSystemsOrder_Spawner". Difference between theese objects is just an instances count, that script will be spawn.

First case:
- Enable "SuccessSystemsOrder_Spawner" and hit play. As result you will be see cloud of 1000 cubes. Theese cubes won't move, because "ModifySpeedSystem" multiply 100f by 0f and get 0f as result.

Second case:
- Enable "IgnoreSystemsOrder_Spawner" and hit play. As result you will be see cloud of 100000 cubes. Theese cubes will be move, that marks the "ModifySpeedSystem" doesn't work correct.

As i understand, when CPU get high loads the systems execution order or Jobs dependency for some reason not respects.