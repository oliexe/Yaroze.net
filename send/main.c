/* main3.c */


#define BPS 115200 
#define STACKP 0x801ffff0

#include <sys\types.h>
#include <libsn.h>
#include <libetc.h>
#include <libgte.h>
#include <libgpu.h>
#include <libgs.h>
#include <libsio.h>
#include <libcd.h>
#include <kernel.h>
#include <setjmp.h>

int dummy=0;

u_long block=0;
u_char block_lo=0;
u_char block_hi=0;



/* prototypen */

char lies (void);
void schreib (char);
void init_sio (void);
u_long GetLongData(void);


u_char header[2048];
static struct XF_HDR * head;

main()
{                             


                int id;
                int i=0;
                int s=0;
                int fixed=-110;
                char success=0;
                u_long sync;
                u_long f_len;
                u_long x_addr;
                u_long count;
                
                char * write_addr;







                PadInit(0);
                PadStop();
                DelSIO();
                StopCallback();
               



                SetVideoMode(1);
                SetDispMask(1);
                ResetGraph (0) ;
                SetGraphReverse(0);


              
                GsInitGraph(320,256,0,0,0);
                GsDefDispBuff(0,0,0,256);
                              
                GsInit3D();

                SetDispMask(1); /*sichtbar*/

                FntLoad (960,256);
                
                SetDumpFnt(id = FntOpen(0,0,320,200,1,1024));
                
                FntPrint("            WAITING FOR PC\n"); 

                FntFlush(-1);









                init_sio();

                
               
                
                while (sync != 99){
                  sync = lies();
                  }


                FntPrint("            Sync received: %d .\n",sync);

                FntFlush(-1);



                for (i=0;i<2048;i++){
                header[i] = lies();
                
                }
               


                x_addr = GetLongData();

                FntPrint("            x_addr: %x \n",x_addr); 
                                   
                write_addr =(char *) GetLongData();

                FntPrint("            write_addr: %x \n",write_addr); 

                f_len = GetLongData();

                FntPrint("            f_len : %x \n",f_len); 

                FntFlush(-1);

                

                for (count=0;count<f_len;count++){
                   *write_addr = lies();
                   write_addr++;


                }


           /*     PadStop(); */
		ResetGraph(3);
                
		StopCallback();

                head = (struct XF_HDR *)header;
		head->exec.s_addr = STACKP;
		head->exec.s_size = 0;
		EnterCriticalSection();
		Exec(&(head->exec),1,0);



}

char lies(void)
{
	char c;
	long sts;

	sts = _sio_control(0,1,0);
	_sio_control(1,1,sts|CR_RTS);		/* RTS:on */
	while(!(_sio_control(0,0,0)&SR_RXRDY));
	c = _sio_control(0,4,0)&0xff;
	sts = _sio_control(0,1,0);
	_sio_control(1,1,sts&(~CR_RTS));	/* RTS:off */
	return c;
}


void schreib(char c)
{
	while((_sio_control(0,0,0)&(SR_TXU|SR_TXRDY))!=(SR_TXU|SR_TXRDY));
	_sio_control(1,4,c);
}

void init_sio(void)
{
	_sio_control(1,2,MR_SB_01|MR_CHLEN_8|0x02); /* 8bit, no-parity, 1 stop-bit */
	_sio_control(1,3,BPS);
	_sio_control(1,1,CR_RXEN|CR_TXEN);	/* RTS:off DTR:off */
}


u_long GetLongData(void)
{

        u_long dat;


        dat = (lies());    
        dat |= (lies()) <<8;
        dat |= (lies()) <<16;
        dat |= (lies()) << 24;
        
       return (dat);
       
}

