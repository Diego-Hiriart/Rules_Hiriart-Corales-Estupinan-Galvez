using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Ball
{            
    public int Health { set; get; }
    public int Lives { set; get; }
    public int Points { set; get; }
    public float Speed { set; get; }
        
    public Ball()
    {
        this.Health = 100;
        this.Lives = 3;
        this.Points = 0;
        this.Speed = 5;
    }

    public void Respawn()
    {
        this.Health = 100;
    }

}
