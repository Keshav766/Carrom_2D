# Carrom_2D

1: First, I started a new 2D project in Unity and created the base of the game using the sprites provided.
2: Added colliders and rigidbody to the pucks and the striker. Along with triggers on the carromboard collectors (holes).
3: Striker Implementation:
   -> Made a circular sprite child to striker for force reference and also made the arrow helper gameobject child to the
      circle to denote the direction of striker release. 
   -> For implementing the player striker movement, I thought of applying force to the striker using raycast2D, but
      after solving multiple errors, it was still not right in terms of the orientation of the force direction and the scale of
      circle and direction. 
   -> So I surfed through the internet and found a better way by using only GetMouseButtonUp and 
      GetMouseButtonDown.
   -> With mouseDown, we can get the vector position where the player starts to drag the striker, and on mouseUp, we get
      the vector position here, the player released the striker.
   -> Using these two values, I can get the direction of force, direction of the arrow helper, magnitude of force, and scale amount
      of reference circle around the circle.
   -> Attached the StrikerHandler script to player striker.
4: AI Striker Implementation:
   -> After player striker, this was easy since I had the striker's initial position, and for the second vector position, I used 
      FindObjectOfType to find the "white puck" on the game board and apply force in that direction.
   -> Attached the EnemyStriker script to Enemy Striker.
5: Created a physicsMaterial2D for applying bounciness, and applied it to all pucks,and both strikers. tweaked the drags accordingly.
6: Created Canvas:
   -> 2 min timer, which on depletion indicated gameover.
   -> Added a slider for player striker movement on the striker line.
   -> Added score text and image for player, AI and fixed the anchors of all canvas objects so that the screen remains scalable.
   -> Made a ScoreHandler script that:
      - When a "white puck" hits the trigger, the AI score is implemented.
        Similarly, when a "black puck" hits the trigger, the player score is implemented . 
      - For "Queen Puck," I used the playerturn bool variable from the GameManager script (NEXT STEP) to increment the score
        of the one that made the shot.
   -> Attached the ScoreHandler script to CarromBoard.
7: GameManager:
   -> It kept track of who is currently taking the shot and enabled and disabled the player and enemy game objects accordingly.
   -> Created an empty object in the scene and attached the GameManager script to it.




