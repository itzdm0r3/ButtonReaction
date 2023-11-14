using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ButtonReaction
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Game World

        // Display font
        SpriteFont font;

        // Game Timer
        int timer;

        // Game world sounds
        SoundEffect dingSound;

        // Gamepad 1 state
        GamePadState pad1;
        GamePadState oldpad1;

       /* int ascore1;
        int bscore1;
        int xscore1;
        int yscore1;*/

        // Create an array to hold player scores
        int[] scores = new int[4];

        // Create array to hold player names and use as lookup table
        string[] names = new string[]
        {
            "Gamepad 1 A WINS",
            "Gamepad 1 B WINS",
            "Gamepad 1 X WINS",
            "Gamepad 1 Y WINS"
        };

        // Create variable to store name of winner
        string winnerName = "";

        // Create player how-to message
        string helpMessage = "Press Start To Begin Game... Hit Button After Sound Plays!";

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load font
            font = Content.Load<SpriteFont>("SpriteFont1");

            // Load sound
            dingSound = Content.Load<SoundEffect>("ding_sound");



        }

        protected override void Update(GameTime gameTime)
        {
            // Get gamepad state
            pad1 = GamePad.GetState(PlayerIndex.One);


            if (pad1.Buttons.Back == ButtonState.Pressed)
            {
                Exit();
            }

            // start a new game
            if (pad1.Buttons.Start == ButtonState.Pressed)
            {
               for(int i = 0; i < 4; i++)
                {
                    scores[i] = 0;
                }
                winnerName = "";
                timer = -120;
            }

            // Update the timer
            timer++;

            // play sound at the start of the game
            if (timer == 0)
            {
                dingSound.Play();
            }

            // if A is pressed and scores[0] is 0 copy the timer
            if (oldpad1.Buttons.A == ButtonState.Released &&
                pad1.Buttons.A == ButtonState.Pressed && scores[0] == 0)
            {
                scores[0] = timer;
            }
            // repeat code for buttons B, X, and Y

            // if B is pressed and scores[1] is 0 copy the timer
            if (oldpad1.Buttons.B == ButtonState.Released &&
                pad1.Buttons.B == ButtonState.Pressed && scores[1] == 0)
            {
                scores[1] = timer;
            }

            // if X is pressed and scores[2] is 0 copy the timer
            if (oldpad1.Buttons.X == ButtonState.Released &&
                pad1.Buttons.X == ButtonState.Pressed && scores[2] == 0)
            {
                scores[2] = timer;
            }

            // if Y is pressed and scores[3] is 0 copy the timer
            if (oldpad1.Buttons.Y == ButtonState.Released &&
                pad1.Buttons.Y == ButtonState.Pressed && scores[3] == 0)
            {
                scores[3] = timer;
            }

            oldpad1 = pad1;
            // repeat for gamepads 2, 3, and 4 to add multiplayer functionality.

            if (timer == 120)
            {
                int winningValue = 120;
                int winnerSubscript = 0;

                for (int i = 0; i < 4; i++)
                {
                    if (scores[i] > 0)
                    {
                        if (scores[i] < winningValue)
                        {
                            winningValue = scores[i];
                            winnerSubscript = i;
                        }
                    }
                }

                if (winningValue != 120)
                {
                    winnerName = names[winnerSubscript] + "\n\n PRESS START TO PLAY AGAIN!";
                }
                else
                {
                    winnerName = "**NO WINNER**\n\n PRESS START TO PLAY AGAIN!";
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(Color.Black);

            // Display and position scores on the screen
            Vector2 displayascore1 = new Vector2(50, 80);
            Vector2 displaybscore1 = new Vector2(200, 80);
            Vector2 displayxscore1 = new Vector2(350, 80);
            Vector2 displayyscore1 = new Vector2(500, 80);

            // Display winner name on the screen
            Vector2 displayWinner = new Vector2(200, 200);

            // Display help message on the screen
            Vector2 displayMessage = new Vector2(50, 0);

            _spriteBatch.Begin();
            _spriteBatch.DrawString(font, helpMessage, displayMessage, Color.White);
            _spriteBatch.DrawString(font, scores[0].ToString(), displayascore1, Color.Green);
            _spriteBatch.DrawString(font, scores[1].ToString(), displaybscore1, Color.Red);
            _spriteBatch.DrawString(font, scores[2].ToString(), displayxscore1, Color.Blue);
            _spriteBatch.DrawString(font, scores[3].ToString(), displayyscore1, Color.Yellow);
            _spriteBatch.DrawString(font, winnerName, displayWinner, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}